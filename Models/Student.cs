using System;
using System.Collections.Generic;

namespace Labb3.Models;

public partial class Student
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public long SocialSecurityNumber { get; set; } 

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
