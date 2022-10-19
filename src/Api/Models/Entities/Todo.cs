namespace InterviewTask.Api.Models.Entities;


public class Todo
{
    public int TodoId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public bool IsDone { get; set; }
}