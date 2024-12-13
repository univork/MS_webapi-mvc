using System;
using System.Collections.Generic;

namespace ExampleMVCApp.Models;

public partial class ProductDiscount
{
    public int? ProductId { get; set; }

    public int? DiscountId { get; set; }
}
