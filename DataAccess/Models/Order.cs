using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }
        public string? Status { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
