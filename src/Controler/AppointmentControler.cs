using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // If using EF Core for database interactions
using System.Threading.Tasks;        // For asynchronous programming

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AppDbContext context;

    public AppointmentsController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        return Ok(await context.Appointments!
            .Include(a => a.Dentist)
            .Include(a => a.Patient)
            .ToListAsync());
    }

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
