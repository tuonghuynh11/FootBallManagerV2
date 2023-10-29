using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface ILeagueSupplierRepository
    {
        Task<IEnumerable<Leaguesupplier>> GetAll();
        Task<Leaguesupplier> GetById(int idSupplier, int idLeague);
        Task Update(Leaguesupplier t);
        Task<bool> Patch(int idSupplier, int idLeague, JsonPatchDocument leagueSupplierModel);
        Task<int> Create(Leaguesupplier leagueSupplier);
        Task<bool> Delete(int idSupplier, int idLeague);
    }
}
