using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Admin.Shoes
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;

        public DeleteModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shoe Shoe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shoes == null)
            {
                return NotFound();
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Shoes == null)
            {
                return NotFound();
            }
            var shoe = await _context.Shoes.FindAsync(id);

            if (shoe != null)
            {
                Shoe = shoe;
                _context.Shoes.Remove(Shoe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
