using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FootBallManagerAPI.Repositories
{
    public class ChuyennhuongRepository : IChuyennhuongRepository
    {
        private readonly FootBallManagerV2Context _context;

        public ChuyennhuongRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<Chuyennhuong> addChuyennhuongAsync(Chuyennhuong chu)
        {
            var newCn = chu;
            _context.Chuyennhuongs.Add(newCn);
            await _context.SaveChangesAsync();
            return newCn;
        }

        public async Task deleteChuyennhuongAsync(int id)
        {
            var deleteCn = _context.Chuyennhuongs!.FirstOrDefault(x => x.Id == id);
            if (deleteCn != null)
            {
                _context.Chuyennhuongs!.Remove(deleteCn);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<Chuyennhuong>> GetAllChuyennhuongAsync()
        {
            var chuyennhuongs = await _context.Chuyennhuongs!.ToListAsync();
            return chuyennhuongs;
        }

        public async Task<Chuyennhuong> GetChuyennhuongAsync(int id)
        {
            var chuyennhuong = await _context.Chuyennhuongs!.FindAsync(id);
            return chuyennhuong;
        }

        public async Task updateChuyennhuongAsync(int id, Chuyennhuong chu)
        {
            if(id == chu.Id)
            {
                var updateChuyennhuong = chu;
                _context.Chuyennhuongs!.Update(updateChuyennhuong);
                await _context.SaveChangesAsync() ;
            }
        }
    }
}
