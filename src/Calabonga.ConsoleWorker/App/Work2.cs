using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.App;

public class Work2 : WorkBase<AddressResult>
{
    public override int OrderIndex => 1;

    public override string Name => GetType().Name;

    public override string DisplayName => GetType().AssemblyQualifiedName!;

    public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1400, cancellationToken);
        return new WorkSuccessReport<AddressResult>(new AddressResult($"{GetType().Name} done"), this);
    }
}
