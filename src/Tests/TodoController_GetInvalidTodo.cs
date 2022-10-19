using InterviewTask.Api.Controllers;
using InterviewTask.Api.Data;
using InterviewTask.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewTask.Api.Tests;

public class TodoController_GetInvalidTodo
{
    public ServiceProvider ServiceProvider;
    public TodoController_GetInvalidTodo()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddDbContext<TodoDbContext>(options => options.UseSqlServer("Server=localhost,5433;Database=TodoDb;User Id=sa;Password=Pas5W0rd"),
                ServiceLifetime.Transient);

        serviceCollection.AddTransient<ITodoService, DefaultTodoService>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
    [Fact]
    public async Task Controller_Returns_NotfoundAsync()
    {
        var todoService = ServiceProvider.GetService<ITodoService>();
        var logger = ServiceProvider.GetService<ILogger<TodosController>>();
        var controller = new TodosController(todoService!, logger!);
        var result = await controller.GetTodoAsync(129837);
        Assert.IsType<NotFoundResult>(result as NotFoundResult);
    }
}