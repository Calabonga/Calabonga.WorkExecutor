# Calabonga.WorkExecutor

[Calabonga.WorkExecutor](https://www.nuget.org/packages/Calabonga.WorkExecutor/) is a nuget-package. It created for automatically do some repeatable works that should return the same result from different sources.

## What's new

### 2025-02-04 v1.0.0 beta.2 

* First prerelease.
* CI/CD for nuget build created

## Why

Just imagine we should receive this data from some remote service:

``` csharp
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
```

We can receive this from **Service1** or **Service2**, for example Geo-data. If **Service1** will not response, then we should send request to **Service2**. Suppose there are many services that can help us to obtain data we need, and we ca use 2 or 3 or 5 alternatives when first (second, etc.) not responded.

## How

Let's create a WorkExecutor that can find an Address from some remote services.

> Sample below is created to demo purposes only.

1. Install nuget 
   ``` powershell
   dotnet add package Calabonga.WorkExecutor
   ``` 
2. Create an `AddressWorkExecutor`:
   ``` csharp
   /// <summary>
   /// The sample about how to create WorkExecutor
   /// </summary>
   public class AddressWorkExecutor : WorkExecutor<AddressResult, IWorkerConfiguration>
   {
       public AddressWorkExecutor(
           IEnumerable<IWork<AddressResult>> works,
           IWorkerConfiguration configuration,
           ILogger<WorkExecutor<AddressResult, IWorkerConfiguration>> logger)
           : base(works, configuration, logger)
       {
       }
   }
   ```
3. Create a works for `WorkExecutor` something like this:
   
   Method `RunWorkAsync` can request to **Service1** API, but in the example, we do some delay to emulate request. You can create two or five implementations for different requests. I created three `Work1`, `Work2` and `Work3`. They are very similar.

   I will show only one of them: 


   ``` csharp
   /// <summary>
   /// Demo Work1 for <see cref="AddressWorkExecutor"/>
   /// </summary>
   public class Work1 : WorkBase<AddressResult>
   {
       public override int OrderIndex => 1;
   
       public override string DisplayName => "Work One";
   
       public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken    cancellationToken)
       {
           await Task.Delay(3000, cancellationToken);
           var random = Random.Shared.Next(0, 100);
   
           return random switch
           {
               <= 40 => new WorkFailedReport<AddressResult>(new InvalidOperationException($"{GetType().   Name} failed with random number {random} <= 40"), this),
               > 41 and <= 50 => new WorkErrorReport<AddressResult>([$"{GetType().Name} failed with random    number 41>= {random} <= 50 "], this),
               > 50 => new WorkSuccessReport<AddressResult>(new AddressResult($"{GetType().Name}    successfully completed."), this),
               _ => new WorkFailedReport<AddressResult>(new InvalidOperationException($"{GetType().Name}    failed with random out of range."), this)
           };
       }
   
       public override TimeSpan Timeout => TimeSpan.FromSeconds(5);
   }
   ```

4. Predefined `DefaultWorkExecutorConfiguration` for our `AddressWorkProcessor` is implementation of the interface `IWorkExecutorConfiguration`. You can use it or create a custom implementation.
5. Register our entities in our Dependency Injection container. It might look something like that:
   ``` csharp
   // container
   var container = ConsoleApp.CreateContainer(x =>
   {
       x.AddSingleton<AddressWorkExecutor>();
       x.AddTransient<IWorkExecutorConfiguration, DefaultWorkExecutorConfiguration>();
       x.AddTransient<IWork<AddressResult>, Work1>();
       x.AddTransient<IWork<AddressResult>, Work2>();
       x.AddTransient<IWork<AddressResult>, Work3>();
   });
   ```
   That's all in preparer process. Now we can use it.
6. Resolve AddressWorkExecutor somewhere in constructor (API, controller, etc.) or use like me below:
   ``` csharp
   var executor = container.GetRequiredService<AddressWorkExecutor>();
   ```
   
   After that, you can start `AddressWorkExecutor` to execute the works registered works.
   
   If the first service does not respond or the data is not valid, than the second service (OrderIndex will use for sorting) will be launched, if the second does not return the correct data, the third will be launched, and so on until the works that were created and registered for our `AddressWorkExecutor` are not ended.
   
   ``` csharp
   await executor.ExecuteAsync(cancellationTokenSource.Token);
   ```

7. When all works done and not address received (`IsSuccess` = `false`), we can check errors something like this:
    ``` csharp
    if (executor is { IsSuccess: false })
    {
        foreach (var error in executor.Errors)
        {
            logger.LogError(error);
        }

        return;
    }
    ```

8. In other case the result `AddressResult` received (`IsSuccess` = `true`), but some errors in previous work we want to see. Is possible! 
    ``` csharp
    if (executor.Errors.Any())
    {
        logger.LogInformation("But some errors occured:");
        foreach (var error in executor.Errors)
        {
            logger.LogWarning($" -> {error}");
        }
    }
    ```

   
9. You can create as many work for WorkExecutor as you want, and WorkExecutors can create as much as you want too. 
10. Have a nice coding, friends!