using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class FootballmatchRepository : IFootballmatchRepository
    {
        private readonly FootBallManagerV2Context _context;

        public FootballmatchRepository(FootBallManagerV2Context context) { 
            _context = context;
        }
        public async Task<Footballmatch> addFootballmatchAsync(Footballmatch footballmatch)
        {
            var newFootballmatch = footballmatch;
            _context.Footballmatches.Add(newFootballmatch);
            await _context.SaveChangesAsync();
            return newFootballmatch;
        }

        public async Task deleteFootballmatchAsync(int id)
        {
            var deleteFootballmatch = _context.Footballmatches!.FirstOrDefault(x => x.Id == id);
            if (deleteFootballmatch != null)
            {
                _context.Footballmatches!.Remove(deleteFootballmatch);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<Footballmatch>> GetAllFootballmatch()
        {
            var footballmatchs = await _context.Footballmatches!.ToListAsync();
            return footballmatchs;
        }

        public async Task<Footballmatch> GetFootballmatch(int id)
        {
            var footballmatch = await _context.Footballmatches!.FindAsync(id);
            return footballmatch;
        }

        public async Task updateFootballmatchAsync(int id, Footballmatch footballmatch)
        {
            if(id == footballmatch.Id)
            {
                var updateFootballmatch = footballmatch;
                _context.Footballmatches!.Update(updateFootballmatch);
                await _context.SaveChangesAsync();
            }
        }
    }
}
