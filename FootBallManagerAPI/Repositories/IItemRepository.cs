using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IItemRepository
    {
        public Task<List<Item>> GetAllItemsAsync();
        public Task<Item> GetItemAsync(int id);
        public Task<int> addItemAsync(Item item);
        public Task updateItemAsync(int id, Item item);
        public Task deleteItemAsync(int id);
    }
}
