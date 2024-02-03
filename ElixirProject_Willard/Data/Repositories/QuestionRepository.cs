using ElixirProject_Willard.Data;
using ElixirProject_Willard.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElixirProject_Willard.Data.Repositories;

public class QuestionRepository
{
    private readonly AppDbContext _context;
    private readonly Random _random = new();

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all questions a given user has answered
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    public IEnumerable<Question> GetAllAnsweredQuestionsForUser(Person person)
    {
        var questionsForPersonWithAnswers = _context.Questions
            .Where(question =>
                _context.Answers.Any(answer =>
                    answer.PersonId == person.Id && answer.QuestionId == question.Id));

        foreach(Question question in questionsForPersonWithAnswers)
        {
            yield return question;
        }
    }

    /// <summary>
    /// Get questions in a random order
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Question> GetAllRandomly()
    {
        // Pull each row and assign a random number.
        // The randome number generation doesn't translate well to sqlite,
        // so we have to do a .ToList() to materialize the result set - and that kills any
        // query optimization benefit we might have gotten from the yield iterator below
        var randomOrder = _context.Questions
                    .Where(q => q.IsActive)
                    .Select(q => new { Question = q, RandomOrder = _random.Next() })
                    .ToList();

        // do the shuffle baby!
        IEnumerable<Question>? orderedResults = randomOrder
            .OrderBy(q => q.RandomOrder)
            .Select(q => q.Question);

        // in some persistance layers, EF is smart enough to properly handle the yield operator,
        // but not sqlite.  TODO refactor if ever moving to another DB
        foreach (Question q in orderedResults)
        {
            yield return q;
        }
    }

}
