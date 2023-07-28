using DataAccess.Models;
using DataAccess.Repository;

namespace DataAccess.ReporitoryImp
{
    public class ShoeRepository : IShoeRepository
    {
        private readonly PRN_Shoes_StoreContext _context;
        public ShoeRepository(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public Shoe? GetShoe(int id)
        {
            return _context.Shoes.FirstOrDefault(x => x.ShoeId == id);
        }
        public void UpdateShoe(Shoe shoe)
        {
            _context.Shoes.Update(shoe);
            _context.SaveChanges();
        }
    }
}
