using DataAccess.Models;
using DataAccess.Repository;

namespace DataAccess.ReporitoryImp
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PRN_Shoes_StoreContext _context;
        public CategoryRepository(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }
    }
}
