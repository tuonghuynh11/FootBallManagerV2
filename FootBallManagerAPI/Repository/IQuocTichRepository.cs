using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IQuocTichRepository
    {
        Task<IEnumerable<Quoctich>> GetAll();
        Task<Quoctich> GetById(int id);
        Task Update(Quoctich t);
        Task<bool> Patch(int id, JsonPatchDocument quoctichModel);
        Task<int> Create(Quoctich quoctich);
        Task<bool> Delete(int id);
    }
}
