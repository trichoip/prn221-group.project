using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IShoeRepository
    {
        Shoe? GetShoe(int id);
        void UpdateShoe(Shoe shoe);
    }
}
