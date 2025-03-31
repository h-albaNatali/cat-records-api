using Microsoft.AspNetCore.Mvc;
using MyProject.Application.Interfaces;
using MyProject.Application.DTOs;

namespace MyProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly IDataUpsertJob _dataUpsertJob;


    public DataController(IDataService dataService,IDataUpsertJob dataUpsertJob)
    {
        _dataService = dataService;
        _dataUpsertJob = dataUpsertJob;

    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _dataService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _dataService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RecordDto dto)
    {
        var created = await _dataService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RecordDto dto)
    {
        var updated = await _dataService.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _dataService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
    
    [HttpPost("fetch-external")]
    public async Task<IActionResult> FetchExternal()
    {
        await _dataUpsertJob.ExecuteAsync();
        return Ok(new { message = "External data fetched and stored successfully." });
    }
}
