using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ElixirProject_Willard.Data.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}
