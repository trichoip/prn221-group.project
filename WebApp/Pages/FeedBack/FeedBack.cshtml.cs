using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.FeedBack
{
    public class FeedBackModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;

        public FeedBackModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            return RedirectToPage("/Shoes/Index");
        }

        [BindProperty]
        public Rating Rating { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int ShoeID, int ID, string Comment, int rating3)
        {
            Rating = new Rating
            {
                ShoeId = ShoeID,
                AccountId = ID,
                Comment = Comment,
                Rating1 = rating3,
                Status = "Processing"
            };
            _context.Add(Rating);
            _context.SaveChanges();

            return RedirectToPage("/Shoes/Details", new { id = ShoeID });
        }
    }
}
