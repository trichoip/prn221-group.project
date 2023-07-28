using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Utils;

namespace WebApp.Pages.Admin.Feedbacks
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;

        public IndexModel(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public PaginatedList<Rating> Rating { get; set; }
        public string CurrentSearchString { get; set; }
        public async Task OnGetAsync(string SearchString, int? PageIndex)
        {
            IQueryable<Rating> RatingIQ = _context.Ratings
                .Include(a => a.Account)
                .Include(s => s.Shoe);
            CurrentSearchString = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                RatingIQ = RatingIQ.Where(s => s.Status.Contains(SearchString));
            }
            var PageSize = 5;
            Rating = await PaginatedList<Rating>.CreateAsync(RatingIQ.AsNoTracking(), PageIndex ?? 1, PageSize);
        }
    }
}
