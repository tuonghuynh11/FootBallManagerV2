using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FootBallManagerAPI.Repository
{
    public class ThamGiaRepository : IThamGiaRepository
    {
        private readonly FootBallManagerV2Context _context;

        public ThamGiaRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<int> Create(Thamgium thamgia)
        {
            _context.Thamgia.Add(thamgia);
            await _context.SaveChangesAsync();
            return thamgia.Idtran;
        }

        public async Task<bool> Delete(int idTran, int idCauthu)
        {
            var thamgia = await _context.Thamgia.FindAsync(idTran, idCauthu);
            if (thamgia == null)
            {
                return false;
            }

            _context.Thamgia.Remove(thamgia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Thamgium>> GetAll()
        {
            if (_context.Thamgia.IsNullOrEmpty())
            {
                return new List<Thamgium>();
            }
            return await _context.Thamgia.Include(t =>t.IdcauthuNavigation).Include(t=>t.IdtranNavigation).ToListAsync();
        }

        public async Task<List<Thamgium>> GetById(int idTran)
        {
            var thamgias = await _context.Thamgia.Where(t => t.Idtran == idTran).Include(t => t.IdcauthuNavigation).Include(t => t.IdtranNavigation).ToListAsync();


            return thamgias;
        }

        public async Task<bool> IsPlayerJoin(int idTran, int idCauthu)
        {
            var thamgia = await _context.Thamgia.FindAsync(idTran, idCauthu);
            return thamgia != null;
        }

        public async Task<bool> Patch(int idTran, int idCauthu, JsonPatchDocument thamGiaModel)
        {

            var thamgia =  _context.Thamgia.Where(t=>t.Idtran==idTran&&t.Idcauthu==idCauthu).FirstOrDefaultAsync();
            if (thamgia == null)
            {
                return false;
            }
            thamGiaModel.ApplyTo(thamgia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Thamgium t)
        {
            _context.Thamgia.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
