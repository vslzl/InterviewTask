using InterviewTask.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewTask.Api.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Todo>().HasKey(p => p.TodoId);
        builder.Entity<Todo>().Property(p => p.Title)
        .IsRequired()
        .HasMaxLength(200);
        builder.Entity<Todo>().HasIndex(p => p.Title)
        .IsUnique(false);

    }

    public DbSet<Todo> Todos { get; set; } = null!;
}