using System;

public class Appointment
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int? DentistId { get; set; }
    public Dentist? Dentist { get; set; }
    public int? PatientId { get; set; }
    public Patient? Patient { get; set; }
    public string? Notes { get; set; }
}
