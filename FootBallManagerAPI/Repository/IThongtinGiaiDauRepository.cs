using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IThongtinGiaiDauRepository
    {
        Task<IEnumerable<Thongtingiaidau>> GetAll();
        Task<IEnumerable<Thongtingiaidau>> GetById(int idGiaiDau);
        Task Update(Thongtingiaidau t);
        Task<bool> Patch(int idGiaiDau,string idDoiBong, JsonPatchDocument thongTinGiaiDauModel);
        Task<int> Create(Thongtingiaidau thongTinGiaiDau);
        Task<bool> Delete(int idGiaiDau, string idDoiBong);
    }
}
