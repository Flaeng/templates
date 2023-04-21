namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public class CreateTodoItemRequest
{
    [Required, MinLength(1), MaxLength(255)]
    public string? Title { get; set; }
    
    [MaxLength(255)] 
    public string? Description { get; set; }
    
    [Required]
    public PriorityLevel? PriorityLevel { get; set; }

    [Required]
    public Guid ListId { get; set; }
}