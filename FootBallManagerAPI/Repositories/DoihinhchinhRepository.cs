using FootBallManagerAPI.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class DoihinhchinhRepository : IDoihinhchinhRepository
    {
        private readonly FootBallManagerV2Context _context;

        public DoihinhchinhRepository( FootBallManagerV2Context context)
        {
            _context = context;
        }
        public async Task<Doihinhchinh> addDoihinhAsync(Doihinhchinh doihinhchinh)
        {
            var newDoihinh = doihinhchinh;
            _context.Doihinhchinhs.Add(newDoihinh);
            await _context.SaveChangesAsync();
            return newDoihinh;
        }

        public async Task deleteDoihinhAsync(string idDoibong, int idCauthu)
        {
            var deleteDoihinh = _context.Doihinhchinhs!.FirstOrDefault(d => d.Iddoibong == idDoibong && d.Idcauthu == idCauthu);
            if(deleteDoihinh != null) {
                _context.Doihinhchinhs!.Remove(deleteDoihinh);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<Doihinhchinh>> GetAllDoihinhAsync()
        {
            var doihinhs = await _context.Doihinhchinhs!.ToListAsync();
            return doihinhs ;
        }

        public async Task<Doihinhchinh> GetDoihinhAsync(string idDoibong, int idCauthu)
        {
            var doihinh = await _context.Doihinhchinhs!.FirstOrDefaultAsync(d => d.Iddoibong == idDoibong && d.Idcauthu == idCauthu);
            return doihinh;
        }

        public async Task updateDoihinhAsync(string idDoibong, int idCauthu, Doihinhchinh doihinhchinh)
        {
            if(idDoibong == doihinhchinh.Iddoibong && idCauthu ==doihinhchinh.Idcauthu) {
                var updateDoihinh = doihinhchinh;
                _context.Doihinhchinhs!.Update(updateDoihinh);
                await _context.SaveChangesAsync();
            }
        }
    }
}
