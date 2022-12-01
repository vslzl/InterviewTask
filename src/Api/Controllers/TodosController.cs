using InterviewTask.Api.Models.Dtos;
using InterviewTask.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.Api.Controllers;

[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoService todoService, ILogger<TodosController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodosAsync(CancellationToken ct = default)
    {
        var todos = await _todoService.GetTodosAsync(ct);
        return Ok(todos);
    }


    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueTodosAsync(CancellationToken ct = default)
    {
        var todos = await _todoService.GetTodosByDateAsync(DateTimeOffset.Now, ct);
        return Ok(todos);
    }

    [HttpGet("{todoId}", Name = nameof(GetTodoAsync))]
    public async Task<IActionResult> GetTodoAsync(int todoId, CancellationToken ct = default)
    {
        var todo = await _todoService.GetTodoAsync(todoId, ct);
        return todo is not null ? Ok(todo) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddTodoAsync([FromBody] TodoDto model, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = await _todoService.AddTodoAsync(model, ct);
        return Created(Url.Link(nameof(GetTodoAsync), new { todoId = dto.TodoId }) ?? string.Empty, dto);
    }

    [HttpPut("{todoId}")]
    public async Task<IActionResult> UpdateTodoAsync(int todoId, [FromBody] TodoDto model, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = await _todoService.UpdateTodoAsync(todoId, model, ct);
        return dto is not null ? Ok(dto) : NotFound();
    }


    [HttpDelete("{todoId}")]
    public async Task<IActionResult> RemoveTodoAsync(int todoId, CancellationToken ct = default)
    {
        var dto = await _todoService.RemoveTodo(todoId, ct);
        return dto is not null ? Ok(dto) : NotFound();
    }

}