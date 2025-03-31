using Microsoft.Extensions.DependencyInjection;
using MyProject.Application.Interfaces;
using MyProject.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;


namespace MyProject.BackgroundJobs.Jobs;

public class DataUpsertJob : IDataUpsertJob
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;

    public DataUpsertJob(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory)
    {
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
    }

    public async Task ExecuteAsync()
    {
    using var scope = _scopeFactory.CreateScope();
    var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();

    var records = await FetchFromApiAsync();
    await dataService.UpsertAsync(records);
    }


    private async Task<List<Record>> FetchFromApiAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ExternalApi");
            var response = await client.GetFromJsonAsync<CatFactResponse>("https://catfact.ninja/facts?limit=20");

            if (response == null || response.Data == null)
                throw new Exception("Failed to fetch external data or data is empty.");

            var records = response.Data
                .Select(x => new Record
                {
                    Title = x.Fact.Length > 20 ? x.Fact.Substring(0, 20) : x.Fact,
                    Description = x.Fact
                })
                .ToList();

            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FetchFromApiAsync] Error: {ex.Message}");
            throw;
        }
    }

    private class CatFactResponse
    {
        public List<CatFact> Data { get; set; } = new();
    }

    private class CatFact
    {
        public string Fact { get; set; } = "";
        public int Length { get; set; }
    }
}
