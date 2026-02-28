using System;
using System.Collections.Generic;

namespace EmpCrud.Models;

public partial class Departments
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employees> Employees { get; set; } = new List<Employees>();
}
