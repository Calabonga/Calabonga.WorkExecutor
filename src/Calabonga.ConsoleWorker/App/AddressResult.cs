namespace Calabonga.ConsoleWorker.App;

/// <summary>
/// Address that we should receive from some service(s)
/// </summary>
public class AddressResult
{
    public AddressResult(string address)
    {
        Address = address;
    }

    public string Address { get; }
}