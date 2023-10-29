using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface ISupplierServiceRepository
    {
        Task<IEnumerable<Supplierservice>> GetAll();
        Task<Supplierservice> GetById(int idService, int idSupplier);
        Task Update(Supplierservice t);
        Task<bool> Patch(int idService, int idSupplier, JsonPatchDocument supplierServiceModel);
        Task<int> Create(Supplierservice supplierService);
        Task<bool> Delete(int idService, int idSupplier);
    }
}
