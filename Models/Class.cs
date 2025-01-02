using System;
using System.Collections.Generic;

namespace Labb3.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
