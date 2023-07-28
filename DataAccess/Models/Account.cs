namespace DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
        }

        public int AccountId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
