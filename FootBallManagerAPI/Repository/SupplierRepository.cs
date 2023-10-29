using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly FootBallManagerV2Context _context;

        public SupplierRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<int> Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier.IdSupplier;
        }

        public async Task<bool> Delete(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return false;
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            if (_context.Suppliers == null)
            {
                return new List<Supplier>();
            }
            return await _context.Suppliers.Include(g => g.Supplierservices).Include(g=>g.Doibongsuppliers).ThenInclude(t => t.DoiBong).Include(g => g.Leaguesuppliers).ToListAsync();
        }

        public async Task<Supplier> GetById(int id)
        {
            var supplier = await _context.Suppliers.Where(g => g.IdSupplier == id).Include(g => g.Supplierservices).Include(g => g.Doibongsuppliers).ThenInclude(t => t.DoiBong).Include(g => g.Leaguesuppliers).FirstOrDefaultAsync();


            return supplier;
        }

        public  async Task<List<FootBallTeamJoin>> GetUnCooperateFootBallTeams(int idSupplier)
        {
            var query = from doiBong in _context.Doibongs
                        where !_context.Doibongsuppliers
                            .Where(ds => ds.IdSupplier == idSupplier)
                            .Select(ds => ds.IdDoiBong)
                            .Contains(doiBong.Id)
                        select new FootBallTeamJoin
                        {
                           ID= doiBong.Id,
                           IDQUOCTICH= doiBong.Idquoctich,
                            THANHPHO=doiBong.Thanhpho,
                            HINHANH=doiBong.Hinhanh,
                            TEN=doiBong.Ten,
                            SOLUONGTHANHVIEN=doiBong.Soluongthanhvien,
                            NGAYTHANHLAP=doiBong.Ngaythanhlap,
                            SANNHA=doiBong.Sannha,
                           SODOCHIENTHUAT =doiBong.Sodochienthuat,
                            GIATRI=doiBong.Giatri
                        };

            List<FootBallTeamJoin> result = query.AsEnumerable().ToList();
            //var footBallTeam =await _context.Database.SqlQueryRaw<FootBallTeamJoin>("SELECT [ID],[IDQUOCTICH],[THANHPHO],[HINHANH],[TEN],[SOLUONGTHANHVIEN],[NGAYTHANHLAP],[SANNHA],[SODOCHIENTHUAT],[GIATRI] FROM [dbo].[DOIBONG] WHERE ID NOT IN (Select idDoiBong FROM DOIBONGSUPPLIER WHERE idSupplier = 1)", new SqlParameter("IdSupplier", idSupplier)).ToListAsync();
            //var footBallTeam =  _context.Database.SqlQuery<Doibong>($"SELECT [ID],[IDQUOCTICH],[THANHPHO],[HINHANH],[TEN],[SOLUONGTHANHVIEN],[NGAYTHANHLAP],[SANNHA],[SODOCHIENTHUAT],[GIATRI]FROM [dbo].[DOIBONG] WHERE ID NOT IN (Select idDoiBong FROM DOIBONGSUPPLIER WHERE idSupplier = 1)").ToList();
            return result;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument supplierModel)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return false;
            }
            supplierModel.ApplyTo(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Supplier t)
        {
            _context.Suppliers.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
