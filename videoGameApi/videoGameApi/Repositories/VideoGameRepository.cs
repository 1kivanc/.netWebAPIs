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

        public async Task AddSync(VideoGame videoGame)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("insert into VideoGames (Title, Publisher, Developer, ReleaseDate) values (@Title, @Publisher, @Developer, @ReleaseDate)", videoGame);

        }

        public async Task<List<VideoGame>> GetAllAsync()
        {
            using var connection = GetConnection();
            var videoGames = await connection.QueryAsync<VideoGame>("select * from VideoGames");
            return videoGames.ToList();
        }

        public async Task<VideoGame> GetByIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var videoGame = await connection.QueryFirstOrDefaultAsync<VideoGame>("select * from VideoGames where id = @Id", new { Id = id });
                return videoGame;
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
