using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAll();
        Task<Supplier> GetById(int id);
        Task<List<FootBallTeamJoin>> GetUnCooperateFootBallTeams(int idSupplier);
        Task Update(Supplier t);
        Task<bool> Patch(int id, JsonPatchDocument supplierModel);
        Task<int> Create(Supplier supplier);
        Task<bool> Delete(int id);
    }
}
