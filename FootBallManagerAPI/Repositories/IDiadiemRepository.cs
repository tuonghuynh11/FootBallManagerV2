using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IDiadiemRepository
    {
        public Task<List<Diadiem>> GetAllDiadiemAsync();
        public Task<Diadiem> GetDiadiemAsync(int id);
        public Task<int> addDiadiemAsync(Diadiem diadiem);
        public Task updateDiadiemAsync(int id, Diadiem diadiem);
        public Task DeleteDiadiemAsync(int id);
    }
}
