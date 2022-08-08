using Sample.Core.Extensions;
using Cocona;
using Sample.Core.UseCase;
using Cocona.Filters;


var builder = CoconaApp.CreateBuilder();
builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.AddCommand(async (ImportProductsUseCase useCase, CoconaAppContext ctx) =>
{
    await useCase.Execute(ctx.CancellationToken);
});

await app.RunAsync().ConfigureAwait(false);

class LoggingFilter : CommandFilterAttribute
{
    private readonly ILogger _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }
    public override async ValueTask<int> OnCommandExecutionAsync(CoconaCommandExecutingContext ctx, CommandExecutionDelegate next)
    {
        _logger.LogInformation("Before: {Name}", ctx.Command.Name);
        try
        {
            return await next(ctx).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {Name}", ctx.Command.Name);
            throw;
        }
        finally
        {
            _logger.LogInformation("End: {Name}", ctx.Command.Name);
        }
    }
}