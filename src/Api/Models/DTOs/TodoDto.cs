using System.ComponentModel.DataAnnotations;

namespace InterviewTask.Api.Models.Dtos;

public class TodoDto
{
    public int TodoId { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public bool IsDone { get; set; }
}