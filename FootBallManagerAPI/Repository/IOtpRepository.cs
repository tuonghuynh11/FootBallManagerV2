using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Repository
{
    public interface IOtpRepository
    {
        Task<IEnumerable<Otp>> GetAll();
        Task<Otp> GetById(int id);
        Task Update(Otp t);
        Task<bool> Patch(int id, JsonPatchDocument otpModel);
        Task<int> Create(Otp otp);
        Task<bool> Delete(int id);
    }
}
