using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IFootballmatchRepository
    {
        public Task<List<Footballmatch>> GetAllFootballmatch();
        public Task<Footballmatch> GetFootballmatch(int id);
        public Task<int> addFootballmatchAsync(Footballmatch footballmatch);
        public Task updateFootballmatchAsync(int id, Footballmatch footballmatch);
        public Task deleteFootballmatchAsync(int id);
    }
}
