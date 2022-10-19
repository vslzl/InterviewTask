using InterviewTask.Api.Data;
using InterviewTask.Api.Models.Entities;

namespace InterviewTask.Api.Services;

public class TodoHostedService : IHostedService
{
    private readonly Random _random = new();
    private readonly IServiceProvider _provider;
    private readonly ILogger<TodoHostedService> _logger;

    public TodoHostedService(IServiceProvider provider, ILogger<TodoHostedService> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SeedDatabaseAsync(cancellationToken);
        }
        catch (System.Exception exception)
        {
            _logger.LogError(exception, "An error occured while generating seed data...");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task SeedDatabaseAsync(CancellationToken stoppingToken)
    {
        var count = _random.Next(1000);
        _logger.LogInformation("{Count} Todo's will be added to inmemory database", count);
        var todos = Enumerable.Range(1, count).Select(p => new Todo
        {
            TodoId = p,
            Title = Faker.Lorem.Sentence(4),
            DueDate = DateTime.Today.AddDays((_random.Next(0, 20) - 10)),
            IsDone = _random.Next(100) > 50
        }).ToArray();

        // since hosted service is a singleton, we need to create a scope to get dbcontext from DI container.
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        context.Todos.AddRange(todos);
        var result = await context.SaveChangesAsync(stoppingToken);
        _logger.LogInformation("{Result} records added to the database", result);
    }
}