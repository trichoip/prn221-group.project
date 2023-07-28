using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Utils;

namespace WebApp.Pages.Shoes
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;
        public PaginatedList<Shoe> Shoe { get; set; }
        public string CurrentSearchString { get; set; }

        public IndexModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string SearchString, int? PageIndex)
        {
            IQueryable<Shoe> ShoeIQ = _context.Shoes.Include(s => s.Brand).Include(s => s.Category);
            CurrentSearchString = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                ShoeIQ = ShoeIQ.Where(s => s.Model.Contains(SearchString));
            }
            var PageSize = 3;
            Shoe = await PaginatedList<Shoe>.CreateAsync(ShoeIQ.AsNoTracking(), PageIndex ?? 1, PageSize);
        }
    }
}
