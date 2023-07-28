using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int? ShoeId { get; set; }
        public int? AccountId { get; set; }
        public int? Rating1 { get; set; }
        public string? Comment { get; set; }
        public string? Status { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Shoe? Shoe { get; set; }
    }
}
