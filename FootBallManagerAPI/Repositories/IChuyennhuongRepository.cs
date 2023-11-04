using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IChuyennhuongRepository
    {
        public Task<List<Chuyennhuong>> GetAllChuyennhuongAsync();
        public Task<Chuyennhuong> GetChuyennhuongAsync(int id);
        public Task<Chuyennhuong> addChuyennhuongAsync(Chuyennhuong chu);
        public Task updateChuyennhuongAsync(int id,  Chuyennhuong chu);
        public Task deleteChuyennhuongAsync(int id);
    }
}