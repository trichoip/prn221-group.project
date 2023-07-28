using DataAccess.Models;
using DataAccess.Repository;

namespace DataAccess.ReporitoryImp
{
    public class RatingRepository : IRatingRepository
    {
        private readonly PRN_Shoes_StoreContext _context;
        public RatingRepository(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }


        public Rating? GetRatingById(int id)
        {
            return _context.Ratings.FirstOrDefault(u => u.RatingId == id);
        }

        public async Task<bool> UpdateRatingStatus(int? ratingId, string status)
        {
            var rating = await _context.Ratings.FindAsync(ratingId);

            if (rating != null)
            {
                if (status == "Reject")
                {
                    rating.Status = "Reject";
                    await _context.SaveChangesAsync();
                    return true;
                }
                else if (status == "Approve")
                {
                    rating.Status = "Approve";
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
