using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FootBallManagerAPI.Repository
{
    public class SupplierServiceRepository : ISupplierServiceRepository
    {
        private readonly FootBallManagerV2Context _context;

        public SupplierServiceRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<int> Create(Supplierservice supplierService)
        {
            _context.Supplierservices.Add(supplierService);
            await _context.SaveChangesAsync();
            return supplierService.IdService;
        }

        public async Task<bool> Delete(int idService, int idSupplier)
        {
            var supplierService = await _context.Supplierservices.FindAsync(idSupplier, idService);
            if (supplierService == null)
            {
                return false;
            }

            _context.Supplierservices.Remove(supplierService);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Supplierservice>> GetAll()
        {
            if (_context.Supplierservices.IsNullOrEmpty())
            {
                return new List<Supplierservice>();
            }
            return await _context.Supplierservices.Include(g => g.IdSupplierNavigation).Include(g => g.IdServiceNavigation).ToListAsync();
        }

        public async Task<Supplierservice> GetById(int idService, int idSupplier)
        {
            var supplierService = await _context.Supplierservices.Where(g => g.IdSupplier == idSupplier && g.IdService==idService).Include(g => g.IdServiceNavigation).Include(g=>g.IdSupplierNavigation).FirstOrDefaultAsync();


            return supplierService;
        }

        public async Task<bool> Patch(int idService, int idSupplier, JsonPatchDocument supplierServiceModel)

        {
            var supplierService = await _context.Supplierservices.Where(sb => sb.IdSupplier == idSupplier&&sb.IdService==idService).FirstOrDefaultAsync();

            //var supplierService = await _context.Supplierservices.FindAsync(idSupplier,idService);
            if (supplierService == null)
            {
                return false;
            }
            supplierServiceModel.ApplyTo(supplierService);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Supplierservice t)
        {
            _context.Supplierservices.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
