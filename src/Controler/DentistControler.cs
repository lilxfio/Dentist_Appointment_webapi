using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

/// <summary>
/// API controller for managing dentists in the system.
/// Provides endpoints to retrieve and add dentist records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DentistsController : ControllerBase
{
    private readonly AppDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DentistsController"/> class.
    /// </summary>
    /// <param name="context">The database context for accessing dentists data.</param>
    public DentistsController(AppDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Retrieves all dentists from the database.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing the list of dentists.</returns>
    [HttpGet]
    public async Task<IActionResult> GetDentists()
    {
        return Ok(await context.Dentists!.ToListAsync());
    }

    /// <summary>
    /// Adds a new dentist to the database.
    /// </summary>
    /// <param name="dentist">The dentist object to add.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the operation. 
    /// Returns a 201 Created response with the newly created dentist.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> AddDentist(Dentist dentist)
    {
        context.Dentists!.Add(dentist);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDentists), new { id = dentist.Id }, dentist);
    }
}
), new { id = dentist.Id }, dentist);
    }
}
