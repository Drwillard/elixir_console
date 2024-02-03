using ElixirProject_Willard.Data;
using ElixirProject_Willard.Data.Models;
using System;
using System.Linq;

namespace ElixirProject_Willard.Data.Repositories;

public class AnswerRepository
{
    private readonly AppDbContext _context;

    public AnswerRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Persist new answer to datastore
    /// </summary>
    /// <param name="answer"></param>
    /// <returns></returns>
    public Answer Add(Answer answer)
    {
        // do null checks
        ArgumentNullException.ThrowIfNull(answer);
        ArgumentException.ThrowIfNullOrWhiteSpace(answer.Text);

        // persist new answer
        _context.Answers.Add(answer);
        _context.SaveChanges();
        return answer;
    }

    /// <summary>
    /// Check if a given answer to a person's question is correct
    /// </summary>
    /// <param name="person"></param>
    /// <param name="question"></param>
    /// <param name="challenge"></param>
    /// <returns></returns>
    public bool CheckAnswer(Person person, Question question, string challenge)
    {
        // do null checks
        ArgumentNullException.ThrowIfNull(person);
        ArgumentNullException.ThrowIfNull(question);
        ArgumentException.ThrowIfNullOrWhiteSpace(challenge);

        // look up answer for person's question
        Answer? isMatch = _context.Answers
            .Where(a => a.PersonId == person.Id)
            .Where(a => a.QuestionId == question.Id)
            .Where(a => a.Text.ToUpper().Trim().Equals(challenge.Trim().ToUpper())).FirstOrDefault();

        // if found, return true
        return isMatch != null;
    }

}
