using System.Net.Http.Json;
using MyProject.Application.Interfaces;
using MyProject.Domain.Entities;

namespace MyProject.BackgroundJobs;

public class DataUpsertJob : IDataUpsertJob
{
    private readonly IDataService _dataService;
    private readonly IHttpClientFactory _httpClientFactory;

    public DataUpsertJob(IDataService dataService, IHttpClientFactory httpClientFactory)
    {
        _dataService = dataService;
        _httpClientFactory = httpClientFactory;
    }

    public async Task ExecuteAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ExternalApi");
            var response = await client.GetFromJsonAsync<CatFactResponse>("https://catfact.ninja/facts?limit=20");

            var records = response?.Data?
                .Where(x => !string.IsNullOrWhiteSpace(x.Fact))
                .Select(x => new Record
                {
                    Title = "Cat Fact",
                    Description = x.Fact ?? "No Description"
                })
                .ToList() ?? new List<Record>();

            await _dataService.UpsertAsync(records);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to fetch external data: " + ex.Message);
            throw;
        }
    }

    private class CatFactResponse
    {
        public List<CatFactEntry>? Data { get; set; }
    }

    private class CatFactEntry
    {
        public string? Fact { get; set; }
        public int Length { get; set; }
    }
}
