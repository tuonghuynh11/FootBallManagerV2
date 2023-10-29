using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repository
{
    public class RoundRepository : IRoundRepository
    {
        private readonly FootBallManagerV2Context _context;

        public RoundRepository(FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<int> Create(Round round)
        {
            _context.Rounds.Add(round);
            await _context.SaveChangesAsync();
            return round.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var round = await _context.Rounds.FindAsync(id);
            if (round == null)
            {
                return false;
            }

            _context.Rounds.Remove(round);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Round>> GetAll()
        {
            if (_context.Rounds == null)
            {
                return new List<Round>();
            }
            return await _context.Rounds.ToListAsync();
        }

        public async Task<Round> GetById(int id)
        {
            var round = await _context.Rounds.Where(g => g.Id == id).FirstOrDefaultAsync();

            return round;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument roundModel)
        {
            var round = await _context.Rounds.FindAsync(id);
            if (round == null)
            {
                return false;
            }
            roundModel.ApplyTo(round);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Round t)
        {
            _context.Rounds.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
