using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FootBallManagerAPI.Repository
{
    public class TapLuyenRepository : ITapLuyenRepository
    {
        private readonly FootBallManagerV2Context _context;

        public TapLuyenRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Tapluyen tapluyen)
        {
            _context.Tapluyens.Add(tapluyen);
            await _context.SaveChangesAsync();
            return tapluyen.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var tapluyen = await _context.Tapluyens.FindAsync(id);
            if (tapluyen == null)
            {
                return false;
            }

            _context.Tapluyens.Remove(tapluyen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Tapluyen>> GetAll()
        {   
            if (_context.Tapluyens.IsNullOrEmpty())
            {
                return new List<Tapluyen>();
            }
            return await _context.Tapluyens.Include(g => g.IddoibongNavigation).Include(g => g.IdnguoiquanlyNavigation).ToListAsync();
        }

        public async Task<Tapluyen> GetById(int id)
        {
            var tapluyen = await _context.Tapluyens.Where(g => g.Id == id).Include(g => g.IddoibongNavigation).Include(g => g.IdnguoiquanlyNavigation).FirstOrDefaultAsync();


            return tapluyen;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument tapLuyenModel)
        {
            var tapluyen = await _context.Tapluyens.Where(sb => sb.Id == id).FirstOrDefaultAsync();

            //var tapluyen = await _context.Tapluyens.FindAsync(id);
            if (tapluyen == null)
            {
                return false;
            }
            tapLuyenModel.ApplyTo(tapluyen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Tapluyen t)
        {
            _context.Tapluyens.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
