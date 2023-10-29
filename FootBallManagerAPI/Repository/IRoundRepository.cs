using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IRoundRepository
    {
        Task<IEnumerable<Round>> GetAll();
        Task<Round> GetById(int id);
        Task Update(Round t);
        Task<bool> Patch(int id, JsonPatchDocument roundModel);
        Task<int> Create(Round round);
        Task<bool> Delete(int id);

    }
}
