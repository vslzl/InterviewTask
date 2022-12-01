using InterviewTask.Api.Data;
using InterviewTask.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseInMemoryDatabase("IMDB_todos");
});

builder.Services.AddScoped<ITodoService, DefaultTodoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<TodoHostedService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClientDev", policyOptions =>
    {
        policyOptions.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Disabled...
// app.UseHttpsRedirection();

app.UseCors("AngularClientDev");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");
// app.MapGet("/api", () => "Hello api World!");
// app.MapGet("/api/*", () => "Hello api * World!");
// app.MapGet("/api/**", () => "Hello api ** World!");

app.MapFallback((IHttpContextAccessor accessor) =>
{

    return "Fallback" + string.Join(',', accessor.HttpContext!.Request.RouteValues.Select(p => p.Key.ToString())) +  "_-_"   + string.Join(',', accessor.HttpContext!.Request.RouteValues.Select(p => p.Value?.ToString()));
});

app.Run();
