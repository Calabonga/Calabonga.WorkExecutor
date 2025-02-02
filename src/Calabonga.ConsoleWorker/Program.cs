using Calabonga.ConsoleWorker;
using Calabonga.ConsoleWorker.App;
using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Configurations;
using Calabonga.Utils.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// container
var container = ConsoleApp.CreateContainer(x =>
{
    x.AddSingleton<DefaultWorkExecutor>();
    x.AddTransient<DefaultWorkerConfiguration>();
    x.AddTransient<IWork<AddressResult>, Work1>();
    x.AddTransient<IWork<AddressResult>, Work2>();
});

var logger = container.GetRequiredService<ILogger<Program>>();
var manager = container.GetRequiredService<DefaultWorkExecutor>();

logger.LogInformation("Starting...");
logger.LogInformation("Total Works: {0}", manager.Works.Count);
logger.LogInformation("Manager timeout: {0}", manager.Configuration.ExecutionTimeout);

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));

AsyncHelper.RunSync(async () => await manager.ExecuteAsync(cancellationTokenSource.Token));

if (manager is { HasReport: false })
{
    logger.LogInformation("No result found");
    return;
}

logger.LogInformation(manager.Result!.Address);
