using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly FootBallManagerV2Context _context;

        public ServiceRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service.IdService;
        }

        public async Task<bool> Delete(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return false;
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            if (_context.Services == null)
            {
                return new List<Service>();
            }
            return await _context.Services.Include(g => g.Supplierservices).Include(g => g.Fieldservices).ToListAsync();
        }

        public async Task<Service> GetById(int id)
        {
            var service = await _context.Services.Where(g => g.IdService == id).Include(g => g.Supplierservices).Include(g => g.Fieldservices).FirstOrDefaultAsync();


            return service;
        }

        public async Task <bool> Patch(int id, JsonPatchDocument serviceModel)
        {
            var service = await _context.Services.Where(sb => sb.IdService == id).FirstOrDefaultAsync();

            //var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return false;
            }
            serviceModel.ApplyTo(service);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Service t)
        {
            _context.Services.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
