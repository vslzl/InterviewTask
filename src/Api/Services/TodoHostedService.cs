using InterviewTask.Api.Data;
using InterviewTask.Api.Models.Entities;

namespace InterviewTask.Api.Services;

public class TodoHostedService : IHostedService
{
    private readonly Random _random = new();
    private readonly IServiceProvider _provider;
    private readonly ILogger<TodoHostedService> _logger;
    private readonly IConfiguration _configuration;

    public TodoHostedService(IServiceProvider provider, ILogger<TodoHostedService> logger, IConfiguration configuration)
    {
        _provider = provider;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            // await CreateDatabaseAsync(cancellationToken);
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

    private async Task CreateDatabaseAsync(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        var counter = 3;
        while (counter > 0)
        {
            try
            {
                var result = await context.Database.EnsureCreatedAsync(stoppingToken);
                if (result)
                    _logger.LogInformation("Database is OK!");
                else
                    _logger.LogCritical("Database creation failed!");
                break;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                _logger.LogInformation("Sql server is not ready...");
                await Task.Delay(TimeSpan.FromSeconds(5 - counter), stoppingToken);
                counter--;
            }
        }

    }

    private async Task SeedDatabaseAsync(CancellationToken stoppingToken)
    {
        // since hosted service is a singleton, we need to create a scope to get dbcontext from DI container.
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        if (!context.Todos.Any() && _configuration["CreateMockData"]=="Y")
        {
            var count = _random.Next(100);
            _logger.LogInformation("{Count} Todo's will be added to inmemory database", count);
            var todos = Enumerable.Range(1, count).Select(p => new Todo
            {
                Title = Faker.Lorem.Sentence(4),
                DueDate = DateTime.Today.AddDays((_random.Next(0, 20) - 10)),
                IsDone = _random.Next(100) > 50
            }).ToArray();

            context.Todos.AddRange(todos);
            var result = await context.SaveChangesAsync(stoppingToken);
            _logger.LogInformation("{Result} records added to the database", result);
        }
    }
}