using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FootBallManagerAPI.Repository
{
    public class DoiBongSupplierRepository : IDoiBongSupplierRepository
    {
        private readonly FootBallManagerV2Context _context;

        public DoiBongSupplierRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Doibongsupplier doiBongSupplier)
        {
            _context.Doibongsuppliers.Add(doiBongSupplier);
            await _context.SaveChangesAsync();
            return doiBongSupplier.IdSupplier;
        }

        public async Task<bool> Delete(int idSupplier, string idDoiBong)
        {
            var doiBongSupplier = await _context.Doibongsuppliers.FindAsync(idSupplier,idDoiBong);
            if (doiBongSupplier == null)
            {
                return false;
            }

            _context.Doibongsuppliers.Remove(doiBongSupplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Doibongsupplier>> GetAll()
        {
            if (_context.Doibongsuppliers.IsNullOrEmpty())
            {
                return new List<Doibongsupplier>();
            }
            var data = await _context.Doibongsuppliers.Include(t => t.IdDoiBongNavigation).Include(t => t.IdSupplierNavigation).ToListAsync();
            return await _context.Doibongsuppliers.Include(t=>t.IdDoiBongNavigation).Include(t => t.IdSupplierNavigation).ToListAsync();
        }

        public async Task<Doibongsupplier> GetById(int idSupplier, string idDoiBong)
        {
            var doiBongSupplier = await _context.Doibongsuppliers.Where(g => g.IdSupplier == idSupplier &&g.IdDoiBong==idDoiBong).Include(t => t.IdDoiBongNavigation).FirstOrDefaultAsync();


            return doiBongSupplier;
        }

        public async Task<bool> Patch(int idSupplier, string idDoiBong, JsonPatchDocument doiBongSupplierModel)
        {
            //var doiBongSupplier = await _context.Doibongsuppliers.FindAsync(idSupplier,idDoiBong);
            var doiBongSupplier = await _context.Doibongsuppliers.Where(sb => sb.IdSupplier == idSupplier && sb.IdDoiBong == idDoiBong).FirstOrDefaultAsync();
            if (doiBongSupplier == null)
            {
                return false;
            }
            doiBongSupplierModel.ApplyTo(doiBongSupplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Doibongsupplier t)
        {
            _context.Doibongsuppliers.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
