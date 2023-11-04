using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IDoibongRepository
    {
        public Task<List<Doibong>> GetAllDoibongAsync();
        public Task<Doibong> GetDoibongAsync(string id);
        public Task<Doibong> addDoibongAsync(Doibong doibong);
        public Task updateDoibongAsync(string id,  Doibong doibong);
        public Task deleteDoibongAsync(string id);
    }
}
