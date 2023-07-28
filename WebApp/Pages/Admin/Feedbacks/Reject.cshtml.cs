using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Admin.Feedbacks
{
    public class RejectModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;
        private readonly IRatingRepository _ratingRepository;

        public RejectModel(PRN_Shoes_StoreContext context, IRatingRepository ratingRepository)
        {
            _context = context;
            _ratingRepository = ratingRepository;
        }

        [BindProperty]
        public Rating Rating { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null || _context.Ratings == null)
            {
                return NotFound();
            }
            Rating = await _context.Ratings
                .AsNoTracking()
                .Include(a => a.Account)
                .Include(s => s.Shoe)
                .FirstOrDefaultAsync(m => m.RatingId == id);

            if (Rating == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string status)
        {
            if (id == null || _context.Ratings == null)
            {
                return NotFound();
            }
            var figure = await _ratingRepository.UpdateRatingStatus(id, status);

            if (figure)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
