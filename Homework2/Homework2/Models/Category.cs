using System;
using System.Collections.Generic;

namespace Homework2.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
