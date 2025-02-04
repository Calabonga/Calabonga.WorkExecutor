﻿using Calabonga.ConsoleWorker;
using Calabonga.ConsoleWorker.App;
using Calabonga.Utils.Extensions;
using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// container
var container = ConsoleApp.CreateContainer(x =>
{
    x.AddSingleton<AddressWorkExecutor>();
    x.AddTransient<IWorkExecutorConfiguration, DefaultWorkExecutorConfiguration>();
    x.AddTransient<IWork<AddressResult>, Work1>();
    x.AddTransient<IWork<AddressResult>, Work2>();
    x.AddTransient<IWork<AddressResult>, Work3>();
});

var logger = container.GetRequiredService<ILogger<Program>>();
var executor = container.GetRequiredService<AddressWorkExecutor>();

logger.LogInformation("Starting WorkExecutor...");
logger.LogInformation("Total Works: {0}", executor.Works.Count);
logger.LogInformation("WorkExecutor configuration timeout: {0}", executor.Configuration.ExecutionTimeout);

var cancellationTokenSource = new CancellationTokenSource();

AsyncHelper.RunSync(async () => await executor.ExecuteAsync(cancellationTokenSource.Token));

if (executor is { IsSuccess: false })
{
    foreach (var error in executor.Errors)
    {
        logger.LogError(error);
    }

    return;
}

logger.LogInformation("WORK SUCCESS: {0}", executor.Result!.Address);

if (executor.Errors.Any())
{
    logger.LogInformation("But some errors occured:");
    foreach (var error in executor.Errors)
    {
        logger.LogWarning($" -> {error}");
    }
}


