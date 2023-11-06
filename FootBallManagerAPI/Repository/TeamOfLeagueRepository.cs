using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FootBallManagerAPI.Repository
{
    public class TeamOfLeagueRepository : ITeamOfLeagueRepository
    {
        private readonly FootBallManagerV2Context _context;

        public TeamOfLeagueRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<int> Create(Teamofleague teamOfLeague)
        {
            _context.Teamofleagues.Add(teamOfLeague);
            await _context.SaveChangesAsync();
            return teamOfLeague.Id;
        }

        public async Task<bool> Delete(int idLeague, string idTeam)
        {
            var teamOfLeague = await _context.Teamofleagues.Where(l=>l.Idgiaidau==idLeague && l.Iddoibong==idTeam).FirstOrDefaultAsync();
            if (teamOfLeague == null)
            {
                return false;
            }

            _context.Teamofleagues.Remove(teamOfLeague);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Teamofleague>> GetAll()
        {
            if (_context.Teamofleagues.IsNullOrEmpty())
            {
                return new List<Teamofleague>();
            }
            return await _context.Teamofleagues.ToListAsync();
        }

        public async Task<List<Teamofleague>> GetById(int idLeague)
        {
            var teamOfLeagues = await _context.Teamofleagues.Where(t => t.Idgiaidau == idLeague).Include(t => t.IddoibongNavigation).ToListAsync();
            return teamOfLeagues;
        }

        public async Task<bool> IsTeamJoinLeague(int idLeague, string idTeam)
        {
            var teamOfLeague = await _context.Teamofleagues.Where(l => l.Idgiaidau == idLeague && l.Iddoibong == idTeam).FirstOrDefaultAsync();
            if (teamOfLeague == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument teamOfLeagueModel)
        {
            var teamOfLeague = await _context.Teamofleagues.Where(sb => sb.Id == id).FirstOrDefaultAsync();

            //var teamOfLeague = await _context.Teamofleagues.FindAsync(id);
            if (teamOfLeague == null)
            {
                return false;
            }
            teamOfLeagueModel.ApplyTo(teamOfLeague);
            await _context.SaveChangesAsync();
            return true;
        }

        public  bool TeamofleagueExists(int id)
        {
            var isExist =  _context.Teamofleagues.Find(id);
            if(isExist == null)
            {
                return false;
            }
            return true;
        }

        public async Task Update(Teamofleague t)
        {
            _context.Teamofleagues.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
