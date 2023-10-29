using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAll();
        Task<Service> GetById(int id);
        Task Update(Service t);
        Task<bool> Patch(int id, JsonPatchDocument serviceModel);
        Task<int> Create(Service service);
        Task<bool> Delete(int id);
    }
}
