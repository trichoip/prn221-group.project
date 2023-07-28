using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.Admin.Shoes
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;

        public CreateModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Shoe Shoe { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Shoes == null || Shoe == null)
            {
                return Page();
            }

            _context.Shoes.Add(Shoe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
