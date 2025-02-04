using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Results;
using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.ConsoleWorker.App;

public class Work3 : WorkBase<AddressResult>
{
    public override int OrderIndex => 3;

    public override string DisplayName => "Work Three";

    public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(3000, cancellationToken);
        var random = Random.Shared.Next(0, 100);

        return random switch
        {
            <= 30 => new WorkFailedReport<AddressResult>(new InvalidOperationException($"Random is {random}"), this),
            > 31 and <= 40 => new WorkErrorReport<AddressResult>([$"Random is {random}"], this),
            > 40 => new WorkSuccessReport<AddressResult>(new AddressResult($"{GetType().Name} DONE"), this),
            _ => new WorkFailedReport<AddressResult>(new InvalidOperationException($"Random is {random}"), this)
        };
    }

    public override TimeSpan Timeout => TimeSpan.FromSeconds(5);
}
