using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IItemtypeRepository
    {
        public Task<List<Itemtype>> GetAllItemtypesAsync();
        public Task<Itemtype> GetItemtypeAsync(int id);
        public Task<Itemtype> addItemtypeAsync(Itemtype itemtype);
        public Task updateItemtypeAsync(int id, Itemtype itemtype);
        public Task deleteItemtypeAsync(int id);
    }
}
