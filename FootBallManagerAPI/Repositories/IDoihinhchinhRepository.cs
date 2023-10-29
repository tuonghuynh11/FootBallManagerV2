using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IDoihinhchinhRepository
    {
        public Task<List<Doihinhchinh>> GetAllDoihinhAsync();
        public Task<Doihinhchinh> GetDoihinhAsync(string idDoibong, int idCauthu);
        public Task<Doihinhchinh> addDoihinhAsync(Doihinhchinh doihinhchinh);
        public Task updateDoihinhAsync(string idDoibong, int idCauthu, Doihinhchinh doihinhchinh);
        public Task deleteDoihinhAsync(string idDoibong, int idCauthu);

    }
}
