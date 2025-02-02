using Calabonga.ConsoleWorker.App;

namespace Calabonga.ConsoleWorker.Workers;

public class Work1 : WorkBase<AddressResult>
{
    public override int OrderIndex => 0;

    public override string Name => GetType().Name;

    public override string DisplayName => GetType().AssemblyQualifiedName!;

    public override async Task<IWorkResult<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1900, cancellationToken);
        return new SuccessWorkResult<AddressResult>(new AddressResult($"{GetType().Name} done"), this);
    }
}
