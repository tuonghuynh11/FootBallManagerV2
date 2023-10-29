using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IThongtinGiaiDauRepository
    {
        Task<IEnumerable<Thongtingiaidau>> GetAll();
        Task<Thongtingiaidau> GetById(int id);
        Task Update(Thongtingiaidau t);
        Task<bool> Patch(int id, JsonPatchDocument thongTinGiaiDauModel);
        Task<int> Create(Thongtingiaidau thongTinGiaiDau);
        Task<bool> Delete(int id);
    }
}
