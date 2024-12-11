using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DentistsController : ControllerBase
{
    private readonly AppDbContext context;

    public DentistsController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDentists()
    {
        return Ok(await context.Dentists!.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddDentist(Dentist dentist)
    {
        context.Dentists!.Add(dentist);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDentists), new { id = dentist.Id }, dentist);
    }
}
