using ElixirProject_Willard.Data.Models;
using ElixirProject_Willard.Data.Repositories;
using System;

namespace ElixirProject_Willard;

public class SurveyService : ISurveyService
{
    private PersonRepository _personRepository;
    private QuestionRepository _questionRepository;
    private AnswerRepository _answerRepository;

    private const string INSTRUCTIONS = "(Leave blank to exit)";

    // how many questions a new user must answer
    private const int ANSWER_THRESHHOLD = 3;

    public SurveyService(QuestionRepository questionRepository,
        PersonRepository personRepository, AnswerRepository answerRepository)
    {
        _questionRepository = questionRepository;
        _personRepository = personRepository;
        _answerRepository = answerRepository;
    }

    public void Run()
    {

        // fetch a name from the user
        string? name = string.Empty;
        while (string.IsNullOrEmpty(name))
        {
            name = GetNameFromUser();
        }

        // does this person exist?
        Person? person = _personRepository.FindByName(name);
        if (person is null)
        {
            person = _personRepository.Add(new Person()
            {
                Name = name,
                IsActive = true
            });

            RunNewUserEnroll(person);
            return;
        }

        // the user exists, so ask them questions
        RunCheckExistingUser(person);

    }

    private static string? GetNameFromUser()
    {
        Console.Clear();
        Console.WriteLine("Hi, what is your name?");
        return Console.ReadLine();
    }

    private void RunNewUserEnroll(Person person)
    {
        // keep track of how many answers have been provided
        int answerCount = 0;

        foreach (Question question in _questionRepository.GetAllRandomly())
        {

            // if we've answered enough, end the loop
            if (answerCount >= ANSWER_THRESHHOLD)
            {
                Console.WriteLine("Security questions complete! Press any key...");
                Console.ReadKey();
                return;
            }


            // display a helpful message for the user
            string message =
                $"You must answer {ANSWER_THRESHHOLD - answerCount} more questions!";

            Console.Clear();
            Console.WriteLine($"{person.Name}, {message}");
            Console.WriteLine(question.Text);
            string? answerText = Console.ReadLine();


            // if we got an empty response, just ignore it 
            if (string.IsNullOrEmpty(answerText))
            {
                continue;
            }

            // otherwise, save the answer
            _answerRepository.Add(new Answer()
            {
                Text = answerText,
                PersonId = person.Id,
                QuestionId = question.Id
            });

            Console.WriteLine("Answer saved! Press any key to continue... ");
            Console.ReadKey();

            answerCount++;

        }

        // present a wrapup message to user
        string finalMessage = answerCount < ANSWER_THRESHHOLD
            ? $"You only answered {answerCount} / {ANSWER_THRESHHOLD} questions, and there are no more available."
            : $"Completed registration!  Press any key....";
        Console.WriteLine(finalMessage);
        Console.ReadKey();

    }

    private void RunCheckExistingUser(Person person)
    {
        Console.WriteLine($"Welcome back, {person.Name}! Press any key to begin answering security questions...");
        Console.ReadKey();

        foreach (Question question in _questionRepository.GetAllAnsweredQuestionsForUser(person))
        {
            Console.Clear();
            Console.WriteLine($"{question.Text} {INSTRUCTIONS}:");
            string? challengeText = Console.ReadLine();

            if (string.IsNullOrEmpty(challengeText)) break;

            if (_answerRepository.CheckAnswer(person, question, challengeText))
            {
                Console.WriteLine("Congratulations! You passed - Press any key...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Incorrect answer. Press any key...");
            Console.ReadKey();

        }

        Console.WriteLine("You've failed to answer any questions correctly. :(");
        Console.ReadKey();

    }
}
