using System;
using System.Collections.Generic;

namespace ef.Models;

public partial class ProductDiscount
{
    public int? ProductId { get; set; }

    public int? DiscountId { get; set; }
}
