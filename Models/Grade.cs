using System;
using System.Collections.Generic;

namespace Labb3.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public int StaffId { get; set; }

    public string Value { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
