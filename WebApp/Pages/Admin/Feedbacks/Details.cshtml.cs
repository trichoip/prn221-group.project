using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Admin.Feedbacks
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;

        public DetailsModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            _context = context;
        }
        public Rating Rating { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Ratings == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(a => a.Account)
                .Include(s => s.Shoe)
                .FirstOrDefaultAsync(r => r.RatingId == id);
            if (rating == null)
            {
                return NotFound();
            }
            else
            {
                Rating = rating;
            }
            return Page();
        }
    }
}
