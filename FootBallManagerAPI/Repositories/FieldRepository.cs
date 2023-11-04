using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly FootBallManagerV2Context _context;

        public FieldRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<Field> addFieldAsync(Field field)
        {
            var newField = field;
            _context.Fields.Add(newField);
            await _context.SaveChangesAsync();
            return newField;
        }

        public async Task deleteFieldAsync(int id)
        {
            var deleteField = _context.Fields!.FirstOrDefault(f => f.IdField == id);
            if(deleteField != null)
            {
                _context.Fields.Remove(deleteField);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<Field>> GetAllFieldsAsync()
        {
            var fields = await _context.Fields!.ToListAsync();
            return fields;
        }

        public async Task<Field> GetFieldAsync(int id)
        {
            var field = await _context.Fields!.FindAsync(id);
            return field;
        }

        public async Task updateFieldAsync(int id, Field field)
        {
            if(field.IdField == id)
            {
                var updateField = field;
                _context.Fields!.Update(updateField);
                await _context.SaveChangesAsync();
            }
        }
    }
}
