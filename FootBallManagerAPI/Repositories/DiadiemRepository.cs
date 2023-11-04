using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class DiadiemRepository : IDiadiemRepository
    {
        private readonly FootBallManagerV2Context _context;

        public DiadiemRepository(FootBallManagerV2Context context) { 
            _context = context;
        }
        public async Task<Diadiem> addDiadiemAsync(Diadiem diadiem)
        {
            var newĐiaiem = diadiem;
            _context.Diadiems.Add(newĐiaiem);
            await _context.SaveChangesAsync();
            return newĐiaiem;
        }

        public async Task DeleteDiadiemAsync(int id)
        {
            var deleteDiadiem = _context.Diadiems!.FirstOrDefault(c => c.Id == id);
            if (deleteDiadiem != null)
            {
                _context.Diadiems!.Remove(deleteDiadiem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Diadiem>> GetAllDiadiemAsync()
        {
            var diadiems = await _context.Diadiems!.ToListAsync();
            return diadiems; ;
        }

        public async Task<Diadiem> GetDiadiemAsync(int id)
        {
            var diadiem = await _context.Diadiems!.FindAsync(id);
            return diadiem;
        }

        public async Task updateDiadiemAsync(int id, Diadiem diadiem)
        {
            if (id == diadiem.Id)
            {
                var updateDiadiem = diadiem;
                _context.Diadiems!.Update(updateDiadiem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
