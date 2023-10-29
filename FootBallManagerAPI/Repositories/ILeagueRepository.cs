using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface ILeagueRepository
    {
        public Task<List<League>> GetAllLeagueListAsync();
        public Task<League> GetLeagueAsync(int id);
        public Task<int> addLeagueAsync(League league);
        public Task updateLeagueAsync(int id, League league);
        public Task deleteLeagueAsync(int id);
    }
}
