using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly FootBallManagerV2Context _context;

        public LeagueRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<League> addLeagueAsync(League league)
        {
            var newLeague = league;
            _context.Leagues.Add(newLeague);
            await _context.SaveChangesAsync();
            return newLeague;
        }

        public async Task deleteLeagueAsync(int id)
        {
            var deleteLeague = _context.Leagues!.FirstOrDefault(x => x.Id == id);
            if(deleteLeague != null)
            {
                _context.Leagues!.Remove(deleteLeague);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<League>> GetAllLeagueListAsync()
        {
            var leagues = await _context.Leagues!.ToListAsync();
            return leagues;
        }

        public async Task<League> GetLeagueAsync(int id)
        {
            var league = await _context.Leagues!.FindAsync(id);
            return league;
        }

        public async Task updateLeagueAsync(int id, League league)
        {
            if(id != league.Id)
            {
                var updateLeague = league;
                _context.Leagues!.Update(updateLeague);
                await _context.SaveChangesAsync();
            }
        }
    }
}
