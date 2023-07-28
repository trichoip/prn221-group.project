using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Shoes = new HashSet<Shoe>();
        }

        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }

        public virtual ICollection<Shoe> Shoes { get; set; }
    }
}
