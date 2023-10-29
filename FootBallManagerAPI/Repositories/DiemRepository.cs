using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class DiemRepository : IDiemRepository
    {
        private readonly FootBallManagerV2Context _context;

        public DiemRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<Diem> addDiemAsync(Diem diem)
        {
            var newDiem = diem;
            _context.Diems.Add(newDiem);
            await _context.SaveChangesAsync();
            return newDiem;
        }

        public async Task deleteDiemAsync(int idGiaidau, string idDoibong)
        {
            var deleteDiem = _context.Diems!.FirstOrDefault(c => c.Idgiaidau == idGiaidau && c.Iddoibong == idDoibong);
            if (deleteDiem != null)
            {
                _context.Diems!.Remove(deleteDiem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Diem>> GetAllDiemAsync()
        {
            var diems = await _context.Diems!.ToListAsync();
            return diems;
        }

        public async Task<Diem> GetDiemAsync(int idGiaidau, string idDoibong)
        {
            var diem = await _context.Diems!.FirstOrDefaultAsync(c => c.Idgiaidau == idGiaidau && c.Iddoibong == idDoibong);
            return diem;
        }

        public async Task updateDiemAsync(int idGiaidau, string idDoibong, Diem diem)
        {
            if (idGiaidau == diem.Idgiaidau && idDoibong == diem.Iddoibong)
            {
                var updateDiem = diem;
                _context.Diems!.Update(updateDiem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
