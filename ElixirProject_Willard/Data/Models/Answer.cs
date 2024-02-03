namespace ElixirProject_Willard.Data.Models;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public int PersonId { get; set; }
    public string? Text { get; set; }
}
