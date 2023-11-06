using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FootBallManagerAPI.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly FootBallManagerV2Context _context;

        public OtpRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Otp otp)
        {
            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();
            return otp.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var otp = await _context.Otps.FindAsync(id);
            if (otp == null)
            {
                return false;
            }

            _context.Otps.Remove(otp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Otp>> GetAll()
        {
            if (_context.Otps.IsNullOrEmpty())
            {
                return new List<Otp>();
            }
            return await _context.Otps.ToListAsync();
        }

        public async Task<Otp> GetById(int id)
        {
            var otp = await _context.Otps.Where(g => g.Id == id).FirstOrDefaultAsync();


            return otp;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument otpModel)
        {
            //var otp = await _context.Otps.FindAsync(id);
            var otp = await _context.Otps.Where(sb => sb.Id==id).FirstOrDefaultAsync();

            if (otp == null)
            {
                return false;
            }
            otpModel.ApplyTo(otp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Otp t)
        {
            _context.Otps.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
