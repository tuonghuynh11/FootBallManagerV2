using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IUserRolesRepository
    {
        Task<IEnumerable<Userrole>> GetAll();
        Task<Userrole> GetById(int id);
        Task Update(Userrole t);
        Task<bool> Patch(int id, JsonPatchDocument UserRoleModel);
        Task<int> Create(Userrole thongtintrandau);
        Task<bool> Delete(int id);
    }
}
