using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface IFieldServiceRepository
    {
        public Task<List<Fieldservice>> GetAllFieldservicesAsync();
        public Task<Fieldservice> GetFieldserviceAsync(int idField, int idService);
        public Task<Fieldservice> addFieldserviceAsync(Fieldservice fieldservice);
        public Task updateFieldServiceAsync(int idField, int idService, Fieldservice fieldservice);
        public Task deleteFieldserviceAsync(int idField, int idService);
    }
}
