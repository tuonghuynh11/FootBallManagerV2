using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IFieldRepository
    {
        public Task<List<Field>> GetAllFieldsAsync();
        public Task<Field> GetFieldAsync(int id);
        public Task<int> addFieldAsync(Field field);
        public Task updateFieldAsync(int id, Field field);
        public Task deleteFieldAsync(int id);
    }
}
