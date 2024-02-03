using System.Collections.Generic;

namespace ElixirProject_Willard.Data.Models;

public class Question
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public bool IsActive { get; set; } = true;
    public IEnumerable<Answer>? Answers { get; set; }

}
