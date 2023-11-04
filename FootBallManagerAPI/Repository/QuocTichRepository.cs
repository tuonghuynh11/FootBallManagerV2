using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repository
{
    public class QuocTichRepository : IQuocTichRepository
    {
        private readonly FootBallManagerV2Context _context;

        public QuocTichRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Quoctich quoctich)
        {
            _context.Quoctiches.Add(quoctich);
            await _context.SaveChangesAsync();
            return quoctich.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var quoctich = await _context.Quoctiches.FindAsync(id);
            if (quoctich == null)
            {
                return false;
            }

            _context.Quoctiches.Remove(quoctich);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Quoctich>> GetAll()
        {
            if (_context.Quoctiches == null)
            {
                return new List<Quoctich>();
            }
            return await _context.Quoctiches.Include(g => g.Doibongs).Include(g => g.Diadiems).ToListAsync();
        }

        public async Task<Quoctich> GetById(int id)
        {
            var quoctich = await _context.Quoctiches.Where(g => g.Id == id).Include(g => g.Doibongs).Include(g => g.Diadiems).FirstOrDefaultAsync();


            return quoctich;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument quoctichModel)
        {
            var quoctich = await _context.Quoctiches.Where(sb => sb.Id == id).FirstOrDefaultAsync();

            //var quoctich = await _context.Quoctiches.FindAsync(id);
            if (quoctich == null)
            {
                return false;
            }
            quoctichModel.ApplyTo(quoctich);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Quoctich t)
        {
            _context.Quoctiches.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
