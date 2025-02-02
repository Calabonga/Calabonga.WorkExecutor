using Calabonga.ConsoleWorker.App;

namespace Calabonga.ConsoleWorker.Workers;

public class Work2 : WorkBase<AddressResult>
{
    public override int OrderIndex => 1;

    public override string Name => GetType().Name;

    public override string DisplayName => GetType().AssemblyQualifiedName!;

    public override async Task<IWorkResult<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1400, cancellationToken);
        return new SuccessWorkResult<AddressResult>(new AddressResult($"{GetType().Name} done"), this);
    }
}
