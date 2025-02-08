using Calabonga.WorkExecutor.Base;

namespace Calabonga.ConsoleWorker.App;

public class AddressResultMetadata : IWorkMetadata
{
    public double Cost { get; set; }

    public int Quantity { get; set; }
}
