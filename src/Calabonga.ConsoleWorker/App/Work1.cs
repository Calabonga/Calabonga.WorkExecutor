using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Results;
using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.ConsoleWorker.App;

/// <summary>
/// Demo Work1 for <see cref="AddressWorkExecutor"/>
/// </summary>
public class Work1 : WorkBase<AddressResult>
{
    public override int OrderIndex => 1;

    public override string DisplayName => "Work One";

    public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(3000, cancellationToken); //Delay for request emulation
        var random = Random.Shared.Next(0, 100);

        return random switch
        {
            <= 40 => new WorkFailedReport<AddressResult>(new InvalidOperationException($"{GetType().Name} failed with random number {random} <= 40"), this),
            > 41 and <= 50 => new WorkErrorReport<AddressResult>([$"{GetType().Name} failed with random number 41>= {random} <= 50 "], this),
            > 50 => new WorkSuccessReport<AddressResult>(new AddressResult($"{GetType().Name} successfully completed."), this),
            _ => new WorkFailedReport<AddressResult>(new InvalidOperationException($"{GetType().Name} failed with random out of range."), this)
        };
    }

    public override TimeSpan Timeout => TimeSpan.FromSeconds(5);
}
