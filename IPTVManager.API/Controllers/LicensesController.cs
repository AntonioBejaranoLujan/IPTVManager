using IPTVManager.Domain.Entities;
using IPTVManager.Services.Interfaces;
using IPTVManager.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IPTVManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LicensesController : ControllerBase
{
    private readonly ILicenseService _licenseService;

    public LicensesController(ILicenseService licenseService)
    {
        _licenseService = licenseService;
    }

    // GET: api/licenses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LicenseDto>>> GetAllLicenses()
    {
        var licenses = await _licenseService.GetAllLicensesAsync();
        return Ok(licenses);
    }

    // GET: api/licenses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LicenseDto>> GetLicense(int id)
    {
        var license = await _licenseService.GetLicenseByIdAsync(id);

        if (license == null)
            return NotFound(new { message = $"Licencia con ID {id} no encontrada" });

        return Ok(license);
    }

    // GET: api/licenses/user/userId123
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<LicenseDto>>> GetUserLicenses(string userId)
    {
        var licenses = await _licenseService.GetUserLicensesAsync(userId);
        return Ok(licenses);
    }

    // GET: api/licenses/expiring/30
    [HttpGet("expiring/{days}")]
    public async Task<ActionResult<IEnumerable<LicenseDto>>> GetExpiringLicenses(int days)
    {
        var licenses = await _licenseService.GetExpiringLicensesAsync(days);
        return Ok(licenses);
    }

    // POST: api/licenses
    [HttpPost]
    public async Task<ActionResult<LicenseDto>> CreateLicense(CreateLicenseDto licenseDto)
    {
        try
        {
            var createdLicense = await _licenseService.CreateLicenseAsync(licenseDto);
            return CreatedAtAction(nameof(GetLicense), new { id = createdLicense.Id }, createdLicense);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/licenses/5
    [HttpPut("{id}")]
    public async Task<ActionResult<LicenseDto>> UpdateLicense(int id, CreateLicenseDto licenseDto)
    {
        try
        {
            var updatedLicense = await _licenseService.UpdateLicenseAsync(id, licenseDto);
            return Ok(updatedLicense);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // DELETE: api/licenses/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLicense(int id)
    {
        var result = await _licenseService.DeleteLicenseAsync(id);

        if (!result)
            return NotFound(new { message = $"Licencia con ID {id} no encontrada" });

        return NoContent();
    }
}