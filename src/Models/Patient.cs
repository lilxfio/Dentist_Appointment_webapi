using System;
using System.Collections.Generic; // Include this namespace for ICollection

/// <summary>
/// Represents a patient in the healthcare system.
/// </summary>
public class Patient
{
    /// <summary>
    /// Gets or sets the unique identifier for the patient.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the patient.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the patient.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the collection of appointments associated with the patient.
    /// </summary>
    public ICollection<Appointment>? Appointments { get; set; }
}

