using ElixirProject_Willard.Data;
using System.Data.SqlTypes;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ElixirProject_Willard.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ElixirProject_Willard.Data.Repositories;

public class PersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;       
    }

    /// <summary>
    /// Find a person by name; return null if not found
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Person? FindByName(string name)
    {
        return _context.Persons
            .Where(x => x.Name.ToUpper().Trim().Equals(name.Trim().ToUpper()))
            .FirstOrDefault();
    }

    /// <summary>
    /// Persist a new person to datastore
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Person Add(Person person)
    {
        // do null checks
        ArgumentNullException.ThrowIfNull(person, nameof(person));
        ArgumentException.ThrowIfNullOrWhiteSpace(person.Name, nameof(person.Name));

        // update the datastore 
        _context.Persons.Add(person);
        _context.SaveChanges();

        // return person
        return person;

    }

}
