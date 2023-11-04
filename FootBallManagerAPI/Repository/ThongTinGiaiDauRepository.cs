using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repository
{
    public class ThongTinGiaiDauRepository : IThongtinGiaiDauRepository
    {
        private readonly FootBallManagerV2Context _context;

        public ThongTinGiaiDauRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Thongtingiaidau thongTinGiaiDau)
        {
            _context.Thongtingiaidaus.Add(thongTinGiaiDau);
            await _context.SaveChangesAsync();
            return thongTinGiaiDau.Idgiaidau;
        }

        public async Task<bool> Delete(int idGiaiDau, string idDoiBong)
        {
            var thongtingiaidau = await _context.Thongtingiaidaus.Where(t=>t.Idgiaidau==idGiaiDau&&t.Iddoibong==idDoiBong).FirstOrDefaultAsync();
            if (thongtingiaidau == null)
            {
                return false;
            }

            _context.Thongtingiaidaus.Remove(thongtingiaidau);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Thongtingiaidau>> GetAll()
        {
            if (_context.Thongtintrandaus == null)
            {
                return new List<Thongtingiaidau>();
            }
            return await _context.Thongtingiaidaus.Include(g=>g.IddoibongNavigation).ToListAsync();
        }

        public async Task<IEnumerable<Thongtingiaidau>> GetById(int idGiaiDau)
        {
            var thongtingiaidau = await _context.Thongtingiaidaus.Where(g=>g.Idgiaidau==idGiaiDau).Include(g => g.IddoibongNavigation).ToListAsync();

          
            return thongtingiaidau;
        }

        public async Task<bool> Patch(int idGiaiDau, string idDoiBong, JsonPatchDocument thongTinGiaiDauModel)
        {
            var thongtingiaidau = _context.Thongtingiaidaus.Where(t => t.Idgiaidau == idGiaiDau&&t.Iddoibong==idDoiBong).FirstOrDefaultAsync();

            //var thongtingiaidau = await _context.Thongtingiaidaus.FindAsync(id);
            if (thongtingiaidau == null)
            {
                return false;
            }
            thongTinGiaiDauModel.ApplyTo(thongtingiaidau);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Thongtingiaidau t)
        {
            _context.Thongtingiaidaus.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
