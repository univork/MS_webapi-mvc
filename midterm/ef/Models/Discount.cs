using System;
using System.Collections.Generic;

namespace ef.Models;

public partial class Discount
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public DateOnly? ValidityPeriod { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
