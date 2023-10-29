using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IThongTinTranDauRepository
    {
        Task< IEnumerable<Thongtintrandau>> GetAll();
        Task<List<Thongtintrandau>> GetById(int id);
        Task Update(Thongtintrandau t);
        Task<bool> Patch(int id, JsonPatchDocument thongTinTranDauModel);
        Task<int> Create(Thongtintrandau thongtintrandau);
        Task<bool> Delete(int id);
    }
}
