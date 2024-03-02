using NZWalks.API.Models.Domain;

namespace NZWalks.API.Services
{
    public interface IWalkService
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, bool isAscending = true, int pageNumber = 1, int pageSize = 20);
        Task<Walk?>GetWalkByIdAsync(Guid id);
        Task<Walk?> UpdateWalkAsync(Walk walk, Guid id);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
