using Dapper;
using System.Data.SqlClient;
using videoGameApi.Models;

namespace videoGameApi.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {
        private readonly IConfiguration _configuration;

        public VideoGameRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<VideoGame>> GetAllAsync()
        {
            using var connection = GetConnection();
            var videoGames = await connection.QueryAsync<VideoGame>("select * from VideoGames");
            return videoGames.ToList();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        }
    }
}
