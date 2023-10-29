using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class FieldserviceRepository : IFieldServiceRepository
    {
        private readonly FootBallManagerV2Context _context;

        public FieldserviceRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<Fieldservice> addFieldserviceAsync(Fieldservice fieldservice)
        {
            var newFieldservice = fieldservice;
            _context.Fieldservices.Add(newFieldservice);
            await _context.SaveChangesAsync();
            return newFieldservice;
        }

        public async Task deleteFieldserviceAsync(int idField, int idService)
        {
            var deleteFieldservice = _context.Fieldservices!.FirstOrDefault(x => x.IdField == idField && x.IdService == idService);
            if (deleteFieldservice != null)
            {
                _context.Fieldservices!.Remove(deleteFieldservice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Fieldservice>> GetAllFieldservicesAsync()
        {
            var fieldservices = await _context.Fieldservices!.ToListAsync();
            return fieldservices;
        }

        public async Task<Fieldservice> GetFieldserviceAsync(int idField, int idService)
        {
            var fieldservice = await _context.Fieldservices!.FirstOrDefaultAsync(x => x.IdField == idField && x.IdService == idService);
            return fieldservice;
        }

        public async Task updateFieldServiceAsync(int idField, int idService, Fieldservice fieldservice)
        {
            if(idField == fieldservice.IdField && idService == fieldservice.IdService)
            {
                var updateFieldservice = fieldservice;
                _context.Fieldservices!.Update(updateFieldservice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
