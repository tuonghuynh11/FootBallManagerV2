using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class ItemtypeRepository : IItemtypeRepository
    {
        private readonly FootBallManagerV2Context _context;

        public ItemtypeRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<Itemtype> addItemtypeAsync(Itemtype itemtype)
        {
            var newItemtype = itemtype;
            _context.Itemtypes.Add(newItemtype);
            await _context.SaveChangesAsync();
            return newItemtype;
        }

        public async Task deleteItemtypeAsync(int id)
        {
            var deleteItemtype = _context.Itemtypes!.FirstOrDefault(x => x.Id == id);
            if (deleteItemtype != null)
            {
                _context.Itemtypes!.Remove(deleteItemtype);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<Itemtype>> GetAllItemtypesAsync()
        {
            var itemtypes = await _context.Itemtypes!.ToListAsync();
            return itemtypes;
        }

        public async Task<Itemtype> GetItemtypeAsync(int id)
        {
            var itemtype = await _context.Itemtypes!.FindAsync(id);
            return itemtype;
        }

        public async Task updateItemtypeAsync(int id, Itemtype itemtype)
        {
            if(id == itemtype.Id)
            {
                var updateItemtype = itemtype;
                _context.Itemtypes!.Update(updateItemtype);
                await _context.SaveChangesAsync();
            }
        }
    }
}
