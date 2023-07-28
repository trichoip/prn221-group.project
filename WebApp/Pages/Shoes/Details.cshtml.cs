using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Shoes
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;

        public DetailsModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public Shoe Shoe { get; set; } = default!;
        public List<Rating> Ratings { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shoes == null)
            {
                return NotFound();
            }
            Ratings = _context.Ratings.Where(c => c.ShoeId == id && c.Status == "Approve").ToList();
            foreach (var rating in Ratings)
            {
                rating.Account = _context.Accounts.FirstOrDefault(a => a.AccountId == rating.AccountId);
            }
            var shoe = await _context.Shoes.Include(s => s.Brand).Include(s => s.Category).FirstOrDefaultAsync(m => m.ShoeId == id);
            if (shoe == null)
            {
                return NotFound();
            }
            else
            {
                Shoe = shoe;
            }
            return Page();
        }
    }
}
