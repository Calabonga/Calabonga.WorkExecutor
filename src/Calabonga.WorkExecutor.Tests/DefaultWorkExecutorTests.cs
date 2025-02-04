using Calabonga.ConsoleWorker.App;
using Calabonga.WorkExecutor.Configurations;
using Calabonga.WorkExecutor.Tests.Fixtures;
using Xunit.Abstractions;

namespace Calabonga.WorkExecutor.Tests;

public class DefaultWorkExecutorTests : IClassFixture<WorkExecutorFixture>
{
    private readonly WorkExecutorFixture _fixture;
    private readonly ITestOutputHelper _outputHelper;

    public DefaultWorkExecutorTests(WorkExecutorFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        var logger = _fixture.GetLogger();
        var executor = new DefaultWorkExecutor([], new DefaultWorkerConfiguration(), logger.Object);

        await executor.ExecuteAsync(CancellationToken.None);

        Assert.True(executor.HasReport);
    }

    [Fact]
    public async Task Test2()
    {
        var logger = _fixture.GetLogger();
        var executor = new DefaultWorkExecutor([], new DefaultWorkerConfiguration(), logger.Object);

        await executor.ExecuteAsync(CancellationToken.None);

        Assert.False(executor.HasWorks);
        _outputHelper.WriteLine(executor.Works.Count.ToString());
    }

    [Fact]
    public async Task Test3()
    {
        const string expected = "No works were registered for DefaultWorkExecutor";
        var logger = _fixture.GetLogger();
        var executor = new DefaultWorkExecutor([], new DefaultWorkerConfiguration(), logger.Object);

        await executor.ExecuteAsync(CancellationToken.None);
        var actual = executor.Errors.First();

        Assert.True(executor.HasReport);
        Assert.NotEmpty(executor.Errors);
        Assert.Equal(expected, actual);

        _outputHelper.WriteLine(string.Join("|", executor.Errors));
    }
}