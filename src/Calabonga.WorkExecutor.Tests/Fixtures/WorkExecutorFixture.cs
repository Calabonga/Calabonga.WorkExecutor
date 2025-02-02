using Calabonga.ConsoleWorker.App;
using Calabonga.ConsoleWorker.Workers;
using Calabonga.ConsoleWorker.Workers.Configurations;
using Microsoft.Extensions.Logging;
using Moq;

namespace Calabonga.WorkExecutor.Tests.Fixtures;

public sealed class WorkExecutorFixture
{
    public Mock<ILogger<WorkExecutor<AddressResult, DefaultWorkerConfiguration>>> GetLogger()
    {
        return new Mock<ILogger<WorkExecutor<AddressResult, DefaultWorkerConfiguration>>>();
    }
}