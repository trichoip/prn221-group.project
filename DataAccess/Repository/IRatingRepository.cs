using DataAccess.Models;

using System.Security.Principal;


namespace DataAccess.Repository
{
    public interface IRatingRepository
    {
        Rating? GetRatingById(int id);
        Task<bool> UpdateRatingStatus(int? ratingId, string status);

    }
}
