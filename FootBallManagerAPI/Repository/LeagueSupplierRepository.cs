using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repository
{
    public class LeagueSupplierRepository : ILeagueSupplierRepository
    {
        private readonly FootBallManagerV2Context _context;

        public LeagueSupplierRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Leaguesupplier leagueSupplier)
        {
            _context.Leaguesuppliers.Add(leagueSupplier);
            await _context.SaveChangesAsync();
            return leagueSupplier.IdSupplier;
        }

        public async Task<bool> Delete(int idSupplier, int idLeague)
        {
            var leagueSupplier = await _context.Leaguesuppliers.FindAsync(idSupplier, idLeague);
            if (leagueSupplier == null)
            {
                return false;
            }

            _context.Leaguesuppliers.Remove(leagueSupplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Leaguesupplier>> GetAll()
        {
            if (_context.Leaguesuppliers == null)
            {
                return new List<Leaguesupplier>();
            }
            return await _context.Leaguesuppliers.ToListAsync();
        }

        public async Task<Leaguesupplier> GetById(int idSupplier, int idLeague)
        {
            var leagueSupplier = await _context.Leaguesuppliers.Where(g => g.IdSupplier == idSupplier && g.IdLeague == idLeague).FirstOrDefaultAsync();


            return leagueSupplier;
        }

        public async Task<bool> Patch(int idSupplier, int idLeague, JsonPatchDocument leagueSupplierModel)
        {
            var leagueSupplier = await _context.Leaguesuppliers.FindAsync(idSupplier, idLeague);
            if (leagueSupplier == null)
            {
                return false;
            }
            leagueSupplierModel.ApplyTo(leagueSupplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Leaguesupplier t)
        {
            _context.Leaguesuppliers.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
