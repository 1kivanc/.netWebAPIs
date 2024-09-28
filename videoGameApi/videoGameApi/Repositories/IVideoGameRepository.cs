using videoGameApi.Models;

namespace videoGameApi.Repositories
{
    public interface IVideoGameRepository
    {
        Task<List<VideoGame>> GetAllAsync();
        Task<VideoGame> GetByIdAsync(int id);
        Task AddSync(VideoGame videoGame);
        Task UpdateAsync(VideoGame videoGame);

        
    }
}
