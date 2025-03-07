﻿using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Reports;
using Calabonga.WorkExecutor.Reports.Base;

namespace Calabonga.ConsoleWorker.App;

/// <summary>
/// Demo Work2 for <see cref="AddressWorkExecutor"/>
/// </summary>
public class Work2 : WorkBase<AddressResult>
{
    public override int OrderIndex => 2;

    public override string? DisplayName => "Work Two";

    public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(3000, cancellationToken); //Delay for request emulation
        var random = Random.Shared.Next(0, 100);

        return random switch
        {
            <= 30 => new WorkFailedReport<AddressResult>(new InvalidOperationException($"Random is {random}"), this),
            > 31 and <= 40 => new WorkErrorReport<AddressResult>([$"Random is {random}"], this),
            > 40 => new WorkSuccessReport<AddressResult>(new AddressResult($"{GetType().Name} DONE"), this),
            _ => new WorkFailedReport<AddressResult>(new InvalidOperationException($"Random is {random}"), this)
        };
    }

    public override TimeSpan Timeout => TimeSpan.FromSeconds(5);

    protected override IWorkMetadata GetMetadata()
    {
        return new AddressResultMetadata
        {
            Cost = 12.5d
        };
    }
}
