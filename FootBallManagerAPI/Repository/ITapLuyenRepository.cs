using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface ITapLuyenRepository
    {
        Task<IEnumerable<Tapluyen>> GetAll();
        Task<Tapluyen> GetById(int id);
        Task Update(Tapluyen t);
        Task<bool> Patch(int id, JsonPatchDocument tapLuyenModel);
        Task<int> Create(Tapluyen tapluyen);
        Task<bool> Delete(int id);
    }
}
