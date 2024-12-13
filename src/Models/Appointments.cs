using System;

/// <summary>
/// Represents an appointment in the healthcare system.
/// </summary>
public class Appointment
{
    /// <summary>
    /// Gets or sets the unique identifier for the appointment.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the appointment.
    /// </summary>
    public DateTime AppointmentDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated dentist.
    /// This property is nullable.
    /// </summary>
    public int? DentistId { get; set; }

    /// <summary>
    /// Gets or sets the dentist associated with the appointment.
    /// This property is nullable.
    /// </summary>
    public Dentist? Dentist { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated patient.
    /// This property is nullable.
    /// </summary>
    public int? PatientId { get; set; }

    /// <summary>
    /// Gets or sets the patient associated with the appointment.
    /// This property is nullable.
    /// </summary>
    public Patient? Patient { get; set; }

    /// <summary>
    /// Gets or sets additional notes or comments about the appointment.
    /// This property is nullable.
    /// </summary>
    public string? Notes { get; set; }
}
