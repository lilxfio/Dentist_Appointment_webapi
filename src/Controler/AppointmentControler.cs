
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

/// <summary>
/// API controller for managing appointments.
/// Provides endpoints to retrieve and schedule appointments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AppDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentsController"/> class.
    /// </summary>
    /// <param name="context">The database context for accessing appointments data.</param>
    public AppointmentsController(AppDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Retrieves all appointments from the database, including associated dentists and patients.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the list of appointments with their related data.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        return Ok(await context.Appointments!
            .Include(a => a.Dentist)
            .Include(a => a.Patient)
            .ToListAsync());
    }

    /// <summary>
    /// Schedules a new appointment in the database.
    /// Ensures that the selected dentist is available at the specified time.
    /// </summary>
    /// <param name="appointment">The appointment object to schedule.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the operation.
    /// Returns a 201 Created response with the newly scheduled appointment or a 400 Bad Request 
    /// if the dentist is unavailable.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> ScheduleAppointment(Appointment appointment)
    {
        if (context.Appointments!.Any(a => a.DentistId == appointment.DentistId &&
                                           a.AppointmentDate == appointment.AppointmentDate))
        {
            return BadRequest("Dentist is not available at this time.");
        }

        context.Appointments!.Add(appointment);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAppointments), new { id = appointment.Id }, appointment);
    }
}
