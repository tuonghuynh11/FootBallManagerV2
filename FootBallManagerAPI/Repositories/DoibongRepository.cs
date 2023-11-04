using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class DoibongRepository : IDoibongRepository
    {
        private readonly FootBallManagerV2Context _context;

        public DoibongRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<Doibong> addDoibongAsync(Doibong doibong)
        {
            var newDoibong = doibong;
            _context.Doibongs.Add(newDoibong);
            await _context.SaveChangesAsync();
            return newDoibong;
        }

        public async Task deleteDoibongAsync(string id)
        {
            var deleteDoibong = _context.Doibongs!.FirstOrDefault(x => x.Id == id);
            if(deleteDoibong != null)
            {
                _context.Doibongs!.Remove(deleteDoibong);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Doibong>> GetAllDoibongAsync()
        {
            var doibongs = await _context.Doibongs!.ToListAsync();
            return doibongs;
        }

        public async Task<Doibong> GetDoibongAsync(string id)
        {
            var doibong = await _context.Doibongs!.FindAsync(id);
            return doibong;
        }

        public async Task updateDoibongAsync(string id, Doibong doibong)
        {
            if(id == doibong.Id)
            {
                var updateDoibong = doibong;
                _context.Doibongs!.Update(updateDoibong);
                await _context.SaveChangesAsync();
            }
        }
    }
}
