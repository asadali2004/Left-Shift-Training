using System;
using System.Collections.Generic;

namespace DBFirstDemo1.Models;

public partial class Restaurants
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;
}
