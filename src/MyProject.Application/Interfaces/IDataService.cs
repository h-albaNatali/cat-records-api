using MyProject.Application.DTOs;
using MyProject.Domain.Entities;

namespace MyProject.Application.Interfaces;

public interface IDataService
{
    Task<IEnumerable<Record>> GetAllAsync();
    Task<Record?> GetByIdAsync(int id);
    Task<Record> CreateAsync(RecordDto dto);
    Task<bool> UpdateAsync(int id, RecordDto dto);
    Task<bool> DeleteAsync(int id);
    Task UpsertAsync(List<Record> records);
}
