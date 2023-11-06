using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FootBallManagerAPI.Repository
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly FootBallManagerV2Context _context;

        public UserRolesRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Userrole userrole)
        {
            _context.Userroles.Add(userrole);
            await _context.SaveChangesAsync();
            return userrole.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var userRole = await _context.Userroles.FindAsync(id);
            if (userRole == null)
            {
                return false;
            }

            _context.Userroles.Remove(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Userrole>> GetAll()
        {
            if (_context.Userroles.IsNullOrEmpty())
            {
                return new List<Userrole>();
            }
            return await _context.Userroles.ToListAsync();
        }

        public async Task<Userrole> GetById(int id)
        {
            var userRole = await _context.Userroles.FindAsync(id);

            //Get information of each teams
         
            return userRole;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument UserRoleModel)
        {
            var userRole = _context.Userroles.Where(t => t.Id == id).FirstOrDefaultAsync();

            //var userRole = await _context.Userroles.FindAsync(id);
            if (userRole == null)
            {
                return false;
            }
            UserRoleModel.ApplyTo(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Userrole t)
        {
            _context.Userroles.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
