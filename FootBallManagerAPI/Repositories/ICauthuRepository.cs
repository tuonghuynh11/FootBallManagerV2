using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface ICauthuRepository
    {
        public Task<List<Cauthu>> GetAllCauthuAsync();
        public Task<Cauthu> getCauthuByIdAsync(int id);
        public Task<Cauthu> addCauthuAsync(Cauthu cauthu);
        public Task updateCauthuAsync(int id, Cauthu cauthu);
        public Task deleteCauthuAsync(int id);
    }
}
