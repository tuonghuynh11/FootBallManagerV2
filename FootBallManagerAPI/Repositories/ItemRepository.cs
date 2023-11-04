using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly FootBallManagerV2Context _context;

        public ItemRepository(FootBallManagerV2Context context) { 
            _context = context;
        }
        public async Task<Item> addItemAsync(Item item)
        {
            var newItem = item;
            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }

        public async Task deleteItemAsync(int id)
        {
            var deleteItem = _context.Items!.FirstOrDefault(x => x.Id == id);
            if (deleteItem != null)
            {
                _context.Items!.Remove(deleteItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            var items = await _context.Items!.ToListAsync();
            return items;
        }

        public async Task<Item> GetItemAsync(int id)
        {
            var item = await _context.Items!.FindAsync(id);
            return item;
        }

        public async Task updateItemAsync(int id, Item item)
        {
            if(id == item.Id)
            {
                var updateItem = item;
                _context.Items!.Update(updateItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
