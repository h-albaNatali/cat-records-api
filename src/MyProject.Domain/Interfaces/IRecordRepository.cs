using MyProject.Domain.Entities;

namespace MyProject.Domain.Interfaces;

public interface IRecordRepository
{
    Task<List<Record>> GetAllAsync();
    Task UpsertAsync(IEnumerable<Record> records);
}
