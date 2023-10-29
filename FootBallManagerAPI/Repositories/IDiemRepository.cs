using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IDiemRepository
    {
        public Task<List<Diem>> GetAllDiemAsync();
        public Task<Diem> GetDiemAsync(int idGiaidau, string idDoibong);
        public Task<Diem> addDiemAsync(Diem diem);
        public Task updateDiemAsync(int idGiaidau, string idDoibong, Diem diem);
        public Task deleteDiemAsync(int idGiaidau, string idDoibong);
    }
}
