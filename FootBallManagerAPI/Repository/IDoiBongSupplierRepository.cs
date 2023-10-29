using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IDoiBongSupplierRepository
    {
        Task<IEnumerable<Doibongsupplier>> GetAll();
        Task<Doibongsupplier> GetById(int idSupplier, string idDoiBong);
        Task Update(Doibongsupplier t);
        Task<bool> Patch(int idSupplier, string idDoiBong, JsonPatchDocument doiBongSupplierModel);
        Task<int> Create(Doibongsupplier doiBongSupplier);
        Task<bool> Delete(int idSupplier, string idDoiBong);
    }
}
