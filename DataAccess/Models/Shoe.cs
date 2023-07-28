namespace DataAccess.Models
{
    public partial class Shoe
    {
        public Shoe()
        {
            OrderItems = new HashSet<OrderItem>();
            Ratings = new HashSet<Rating>();
        }

        public int ShoeId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Sku { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
