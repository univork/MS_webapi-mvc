using System;
using System.Collections.Generic;

namespace Homework2.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
