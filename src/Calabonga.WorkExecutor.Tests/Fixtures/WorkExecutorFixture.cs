using Calabonga.ConsoleWorker.App;
using Calabonga.WorkExecutor.Configurations;
using Microsoft.Extensions.Logging;
using Moq;

namespace Calabonga.WorkExecutor.Tests.Fixtures;

public sealed class WorkExecutorFixture
{
    public Mock<ILogger<WorkExecutor<AddressResult, DefaultConfiguration>>> GetLogger()
    {
        return new Mock<ILogger<WorkExecutor<AddressResult, DefaultConfiguration>>>();
    }
}
