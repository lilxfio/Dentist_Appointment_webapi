using System;
using System.Collections.Generic; // Include this namespace for ICollection

/// <summary>
/// Represents a dentist in the healthcare system.
/// </summary>
public class Dentist
{
    /// <summary>
    /// Gets or sets the unique identifier for the dentist.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the dentist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the specialty of the dentist.
    /// </summary>
    public string? Specialty { get; set; }

    /// <summary>
    /// Gets or sets the collection of appointments associated with the dentist.
    /// </summary>
    public ICollection<Appointment>? Appointments { get; set; }
}
