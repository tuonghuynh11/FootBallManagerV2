using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class HuanluyenvienRepository : IHuanluyenvienRepository
    {
        private readonly FootBallManagerV2Context _context;

        public HuanluyenvienRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> addHlvAsync(Huanluyenvien hlv)
        {
            var newHlv = hlv;
            _context.Huanluyenviens.Add(newHlv);
            await _context.SaveChangesAsync();
            return hlv.Id;
        }

        public async Task deleteHlvAsync(int id)
        {
            var deleteHlv = _context.Huanluyenviens!.FirstOrDefault(h => h.Id == id);
            if (deleteHlv != null)
            {
                _context.Huanluyenviens!.Remove(deleteHlv);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Huanluyenvien>> GetAllHlvAsync()
        {
            var hlvs = await _context.Huanluyenviens!.ToListAsync();
            return hlvs;
        }

        public async Task<Huanluyenvien> GetHuanluyenvienAsync(int id)
        {
            var hlv = await _context.Huanluyenviens!.FindAsync(id);
            return hlv;
        }

        public async Task updateHlvAsync(int id, Huanluyenvien hlv)
        {
            if(id == hlv.Id)
            {
                var updateHlv = hlv;
                _context.Huanluyenviens!.Update(updateHlv);
                await _context.SaveChangesAsync();
            }
        }
    }
}
