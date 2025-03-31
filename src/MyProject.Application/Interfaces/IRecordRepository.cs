using MyProject.Domain.Entities;

namespace MyProject.Application.Interfaces;

public interface IRecordRepository
{
    Task<List<Record>> GetAllAsync();
    Task<Record?> GetByIdAsync(int id);
    Task<Record> CreateAsync(Record record);
    Task UpdateAsync(Record record);
    Task DeleteAsync(Record record);
    Task UpsertAsync(List<Record> records);
}
