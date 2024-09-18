using videoGameApi.Models;

namespace videoGameApi.Repositories
{
    public interface IVideoGameRepository
    {
        Task<List<VideoGame>> GetAllAsync();
    }
}
