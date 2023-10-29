using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repository
{
    public class ThongTinTranDauRepository : IThongTinTranDauRepository
    {
        private readonly FootBallManagerV2Context _context;

        public ThongTinTranDauRepository(FootBallManagerV2Context context) {
            _context = context;
        }

        public async Task<int> Create(Thongtintrandau thongtintrandau)
        {
             _context.Thongtintrandaus.Add(thongtintrandau);
            await _context.SaveChangesAsync();
            return thongtintrandau.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var thongtintrandau = await _context.Thongtintrandaus.FindAsync(id);
            if (thongtintrandau == null)
            {
                return false;
            }

            _context.Thongtintrandaus.Remove(thongtintrandau);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Thongtintrandau>> GetAll()
        {
            if (_context.Thongtintrandaus == null)
            {
                return new List<Thongtintrandau>() ;
            }
            return await _context.Thongtintrandaus.ToListAsync();
        }

        public async Task<List<Thongtintrandau>> GetById(int idTranDau)
        {
            var thongtintrandau = await _context.Thongtintrandaus.Where(m => m.Idtrandau == idTranDau).Include(m => m.IddoibongNavigation).Include(m => m.IdtrandauNavigation).ToListAsync();

            ////Get information of each teams
            //var db1 = await _context.Doibongs.FindAsync(thongtintrandau[0].Iddoibong);
            //var db2 = await _context.Doibongs.FindAsync(thongtintrandau[1].Iddoibong);
            return thongtintrandau;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument thongTinTranDauModel)
        {
            var thongtintrandau = await _context.Thongtintrandaus.FindAsync(id);
            if (thongtintrandau == null)
            {
                return false;
            }
            thongTinTranDauModel.ApplyTo(thongtintrandau);
            await _context.SaveChangesAsync();
            return true;
        }

        public async  Task Update(Thongtintrandau t)
        {
            _context.Thongtintrandaus.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
