using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface ITeamOfLeagueRepository
    {
        Task<IEnumerable<Teamofleague>> GetAll();
        Task<List<Teamofleague>> GetById(int idLeague);
        bool TeamofleagueExists(int id);
        Task Update(Teamofleague t);
        Task<bool> Patch(int id, JsonPatchDocument teamOfLeagueModel);
        Task<int> Create(Teamofleague teamOfLeague);
        Task<bool> Delete(int idLeague, string idTeam);

        Task<bool> IsTeamJoinLeague(int idLeague, string idTeam);
    }
}
