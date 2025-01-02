using System;
using System.Collections.Generic;

namespace Labb3.Models;

public partial class Staff
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int StaffId { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Role Role { get; set; } = null!;
}
