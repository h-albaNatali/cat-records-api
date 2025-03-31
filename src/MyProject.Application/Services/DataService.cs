using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Domain.Entities;
    
namespace MyProject.Application.Services;

public class DataService : IDataService
{
    private readonly IRecordRepository _repository;

    public DataService(IRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Record>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Record?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Record> CreateAsync(RecordDto dto)
    {
        var record = new Record
        {
            Title = dto.Title,
            Description = dto.Description
        };

        return await _repository.CreateAsync(record);
    }

    public async Task<bool> UpdateAsync(int id, RecordDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return false;

        existing.Title = dto.Title;
        existing.Description = dto.Description;

        await _repository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return false;

        await _repository.DeleteAsync(existing);
        return true;
    }

    public async Task UpsertAsync(List<Record> records)
    {
        await _repository.UpsertAsync(records);
    }
}
