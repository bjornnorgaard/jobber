using TickerQ.Utilities.Base;

namespace Jobber.Tick;

public class TickWorker
{
    [TickerFunction("HelloWorld", cronExpression: "0 * * * * *")]
    public Task HelloWorld(TickerFunctionContext context, CancellationToken ct)
    {
        Console.WriteLine($"Hello from TickerQ! Job ID: {context.Id}");
        return Task.CompletedTask;
    }
}