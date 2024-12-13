using Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents the application's database context.
/// Provides access to the database sets for managing dentists, patients, and appointments.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the database set for dentists.
    /// </summary>
    public DbSet<Dentist>? Dentists { get; set; }

    /// <summary>
    /// Gets or sets the database set for patients.
    /// </summary>
    public DbSet<Patient>? Patients { get; set; }

    /// <summary>
    /// Gets or sets the database set for appointments.
    /// This property is virtual to allow mocking or overriding in tests.
    /// </summary>
    public virtual DbSet<Appointment>? Appointments { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class 
    /// with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the database context options.
    /// This method is typically overridden for non-DI scenarios where the connection 
    /// string or provider needs to be set directly in the code.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to configure.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Example:
        // optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}
