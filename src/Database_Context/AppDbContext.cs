using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Dentist>? Dentists { get; set; }
    public DbSet<Patient>? Patients { get; set; }
    public virtual DbSet<Appointment>? Appointments { get; set; }

    // Constructor that accepts DbContextOptions
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // This method can still be used to configure the connection string if needed
    // But it's typically unnecessary if you're using DI, since you pass the connection string via the DI container.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // You can still configure the connection string here for non-DI scenarios.
        // optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}
