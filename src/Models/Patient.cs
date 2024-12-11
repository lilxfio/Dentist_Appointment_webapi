using System;

public class Patient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
}

