using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.App;

public class Work1 : WorkBase<AddressResult>
{
    public override int OrderIndex => 1;

    public override string DisplayName => "Work One";

    public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1500, cancellationToken);
        var random = Random.Shared.Next(0, 100);

        return random switch
        {
            <= 30 => new WorkFailedReport<AddressResult>(new InvalidOperationException($"Random is {random}"), this),
            > 31 and <= 40 => new WorkErrorReport<AddressResult>(this, [$"Random is {random}"]),
            > 40 => new WorkSuccessReport<AddressResult>(new AddressResult($"{GetType().Name} DONE"), this),
            _ => new WorkFailedReport<AddressResult>(new InvalidOperationException($"Random is {random}"), this)
        };
    }

    public override TimeSpan Timeout => TimeSpan.FromSeconds(5);
}
