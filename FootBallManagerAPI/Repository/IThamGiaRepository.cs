using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IThamGiaRepository
    {
        Task<IEnumerable<Thamgium>> GetAll();
        Task<List<Thamgium>> GetById(int idTran);
        Task Update(Thamgium t);
        Task<bool> Patch(int idTran, int idCauthu,JsonPatchDocument thamGiaModel);
        Task<int> Create(Thamgium thamgia);
        Task<bool> Delete(int idTran, int idCauthu);
        Task<bool> IsPlayerJoin(int idTran, int idCauthu);
    }
}
