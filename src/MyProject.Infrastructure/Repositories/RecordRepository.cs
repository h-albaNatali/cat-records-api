using Microsoft.EntityFrameworkCore;
using MyProject.Application.Interfaces;
using MyProject.Domain.Entities;

namespace MyProject.Infrastructure.Repositories;

public class RecordRepository : IRecordRepository
{
    private readonly AppDbContext _context;

    public RecordRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Record>> GetAllAsync()
    {
        return await _context.Records.ToListAsync();
    }

    public async Task<Record?> GetByIdAsync(int id)
    {
        return await _context.Records.FindAsync(id);
    }

    public async Task<Record> CreateAsync(Record record)
    {
        _context.Records.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task UpdateAsync(Record record)
    {
        _context.Records.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Record record)
    {
        _context.Records.Remove(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpsertAsync(List<Record> records)
    {
        foreach (var record in records)
        {
            var existing = await _context.Records.FirstOrDefaultAsync(r => r.Id == record.Id);

            if (existing != null)
            {
                existing.Title = record.Title;
                existing.Description = record.Description;
                _context.Records.Update(existing);
            }
            else
            {
                _context.Records.Add(record);
            }
        }

        await _context.SaveChangesAsync();
    }
}
