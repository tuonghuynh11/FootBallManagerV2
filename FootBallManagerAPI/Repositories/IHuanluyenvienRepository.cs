using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IHuanluyenvienRepository
    {
        public Task<List<Huanluyenvien>> GetAllHlvAsync();
        public Task<Huanluyenvien> GetHuanluyenvienAsync(int id);
        public Task<Huanluyenvien> addHlvAsync(Huanluyenvien hlv);
        public Task updateHlvAsync(int id, Huanluyenvien hlv);
        public Task deleteHlvAsync(int id);
    }
}
