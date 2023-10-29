using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FootBallManagerAPI.Repositories
{
    public class CauthuRepository : ICauthuRepository
    {
        private readonly FootBallManagerV2Context _context;

        public CauthuRepository(FootBallManagerV2Context context) {
            _context = context;
        }   
        public async Task<int> addCauthuAsync(Cauthu cauthu)
        {
            var newCauthu = cauthu;
            _context.Cauthus.Add(newCauthu);
            await _context.SaveChangesAsync();
            return newCauthu.Id;
        }

        public async Task deleteCauthuAsync(int id)
        {
            var deleteCauthu = _context.Cauthus!.FirstOrDefault(c => c.Id == id);
            if(deleteCauthu != null)
            {
                _context.Cauthus!.Remove(deleteCauthu);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<Cauthu>> GetAllCauthuAsync()
        {
            var cauthus = await _context.Cauthus!.ToListAsync();
            return cauthus;
        }

        public async Task<Cauthu> getCauthuByIdAsync(int id)
        {
            var cauthu = await _context.Cauthus!.FindAsync(id);
            return cauthu;
        }

        public async Task updateCauthuAsync(int id, Cauthu cauthu)
        {
            if(id == cauthu.Id)
            {
                var updateCauthu = cauthu;
                _context.Cauthus!.Update(updateCauthu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
