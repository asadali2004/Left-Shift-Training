using System;
using System.Collections.Generic;

namespace EmpCrud.Models;

public partial class Products
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public virtual Categories Category { get; set; } = null!;
}
