using DataAccess.Models;
using DataAccess.Repository;

namespace DataAccess.ReporitoryImp
{
    public class BrandRepository : IBrandRepository
    {
        private readonly PRN_Shoes_StoreContext _context;
        public BrandRepository(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }
    }
}
