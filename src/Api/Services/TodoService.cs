using AutoMapper;
using InterviewTask.Api.Data;
using InterviewTask.Api.Models.Dtos;
using InterviewTask.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewTask.Api.Services;

public interface ITodoService
{
    Task<TodoDto[]> GetTodosAsync(CancellationToken ct);
    Task<TodoDto[]> GetTodosByDateAsync(DateTimeOffset dateRef, CancellationToken ct);
    Task<TodoDto> AddTodoAsync(TodoDto todo, CancellationToken ct);
    Task<TodoDto?> UpdateTodoAsync(int todoId, TodoDto todo, CancellationToken ct);
    Task<TodoDto?> RemoveTodo(int todoId, CancellationToken ct);
    Task<TodoDto?> MarkAsDoneAsync(int todoId, CancellationToken ct);
    Task<TodoDto?> GetTodoAsync(int todoId, CancellationToken ct);
}

public class DefaultTodoService : ITodoService
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<DefaultTodoService> _logger;
    private readonly IMapper _mapper;

    public DefaultTodoService(ILogger<DefaultTodoService> logger, TodoDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TodoDto> AddTodoAsync(TodoDto todo, CancellationToken ct)
    {
        _logger.LogDebug("New todo will be added to repository");
        var entity = _mapper.Map<Todo>(todo);
        _dbContext.Todos.Add(entity);
        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<TodoDto>(entity);
    }

    public async Task<TodoDto?> GetTodoAsync(int todoId, CancellationToken ct)
    {
        _logger.LogDebug("Getting todo with id:{TodoId} from repository", todoId);
        var entity = await _dbContext.Todos.AsNoTracking().SingleOrDefaultAsync(p => p.TodoId == todoId, ct);
        return _mapper.Map<TodoDto>(entity);
    }

    public async Task<TodoDto[]> GetTodosAsync(CancellationToken ct)
    {
        _logger.LogDebug("Getting all todos from repository");
        var entities = await _dbContext.Todos.AsNoTracking().ToArrayAsync(ct);
        return _mapper.Map<TodoDto[]>(entities);
    }

    public async Task<TodoDto[]> GetTodosByDateAsync(DateTimeOffset dateRef, CancellationToken ct)
    {
        _logger.LogDebug("Getting all todos from repository filtered by {DueDate}", dateRef);
        var entities = await _dbContext.Todos.AsNoTracking().Where(p => p.DueDate < dateRef && p.IsDone == false).ToArrayAsync(ct);
        return _mapper.Map<TodoDto[]>(entities);
    }

    public async Task<TodoDto?> MarkAsDoneAsync(int todoId, CancellationToken ct)
    {
        _logger.LogDebug("Marking todo with Id:{TodoId} as done", todoId);
        var entity = await _dbContext.Todos.FindAsync(todoId);
        if (entity is null)
        {
            return null;
        }
        entity.IsDone = true;
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TodoDto>(entity);
    }

    public async Task<TodoDto?> RemoveTodo(int todoId, CancellationToken ct)
    {
        _logger.LogDebug("Removing todo with Id:{TodoId}", todoId);
        var entity = await _dbContext.Todos.FindAsync(todoId);
        if (entity is null)
        {
            return null;
        }
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TodoDto>(entity);
    }

    public async Task<TodoDto?> UpdateTodoAsync(int todoId, TodoDto todo, CancellationToken ct)
    {
        _logger.LogDebug("Updating todo with Id:{TodoId}", todoId);
        var entity = await _dbContext.Todos.FindAsync(todoId);
        if (entity is null)
        {
            return null;
        }
        _mapper.Map(todo, entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TodoDto>(entity);
    }
}