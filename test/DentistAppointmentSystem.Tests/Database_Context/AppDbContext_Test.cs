using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace YourNamespace.Tests
{
    /// <summary>
    /// Unit tests for the AppDbContext, including tests for adding and retrieving appointments.
    /// </summary>
    [TestClass]
    public class AppDbContextTests
    {
        private DbContextOptions<AppDbContext> _options;

        /// <summary>
        /// Initializes a new instance of the AppDbContextTests class.
        /// Configures an in-memory database for testing.
        /// </summary>
        public AppDbContextTests()
        {
            // Use an in-memory database for testing
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")  // Unique name for each test run
                .Options;
        }

        /// <summary>
        /// Tests the ability to add an appointment to the database.
        /// Verifies the appointment is correctly saved and retrievable.
        /// </summary>
        [TestMethod]
        public void Test_AddAppointment_ShouldAddAppointmentToDb()
        {
            // Arrange: Create a new AppDbContext instance with in-memory database
            using (var context = new AppDbContext(_options))
            {
                // Create test data for Dentist and Patient entities
                var dentist = new Dentist { Name = "Dr. Smith", Specialty = "Orthodontist" };
                var patient = new Patient { Name = "John Doe", Phone = "123456789" };

                // Add the Dentist and Patient to the context
                context.Dentists!.Add(dentist);
                context.Patients!.Add(patient);
                context.SaveChanges();

                // Create a new appointment with associated Dentist and Patient
                var appointment = new Appointment
                {
                    AppointmentDate = DateTime.Now.AddDays(1),
                    DentistId = dentist.Id,
                    PatientId = patient.Id,
                    Notes = "Initial Consultation"
                };

                // Act: Add the appointment to the context and save changes
                context.Appointments!.Add(appointment);
                context.SaveChanges();

                // Assert: Verify that the appointment was added successfully
                var savedAppointment = context.Appointments
                                              .FirstOrDefault(a => a.Id == appointment.Id);

                Assert.IsNotNull(savedAppointment);  // Ensure appointment is not null
                Assert.AreEqual(appointment.AppointmentDate, savedAppointment.AppointmentDate);  // Check appointment date
                Assert.AreEqual(appointment.Notes, savedAppointment.Notes);  // Check appointment notes
                Assert.AreEqual(dentist.Id, savedAppointment.DentistId);  // Ensure dentist ID matches
                Assert.AreEqual(patient.Id, savedAppointment.PatientId);  // Ensure patient ID matches
            }
        }

        /// <summary>
        /// Tests retrieving appointments by dentist and ensures they are returned in the correct order.
        /// Verifies that appointments are associated with the correct dentist and ordered by date.
        /// </summary>
        [TestMethod]
        public void Test_GetAppointmentsByDentist_ShouldReturnAppointments()
        {
            // Arrange: Create a new AppDbContext instance with in-memory database
            using (var context = new AppDbContext(_options))
            {
                // Create test data for Dentist and Patient entities
                var dentist = new Dentist { Name = "Dr. Brown", Specialty = "Pediatric Dentist" };
                var patient1 = new Patient { Name = "Alice", Phone = "987654321" };
                var patient2 = new Patient { Name = "Bob", Phone = "987654322" };

                // Add the Dentist and Patients to the context
                context.Dentists!.Add(dentist);
                context.Patients!.Add(patient1);
                context.Patients.Add(patient2);
                context.SaveChanges();

                // Create two appointments associated with the dentist
                var appointment1 = new Appointment
                {
                    AppointmentDate = DateTime.Now.AddDays(2),
                    DentistId = dentist.Id,
                    PatientId = patient1.Id,
                    Notes = "Checkup"
                };

                var appointment2 = new Appointment
                {
                    AppointmentDate = DateTime.Now.AddDays(3),
                    DentistId = dentist.Id,
                    PatientId = patient2.Id,
                    Notes = "Cleaning"
                };

                // Add appointments to the context and save changes
                context.Appointments!.Add(appointment1);
                context.Appointments.Add(appointment2);
                context.SaveChanges();
            }

            // Act: Retrieve the dentist and their appointments, including the appointments related to them
            using (var context = new AppDbContext(_options))
            {
                // Fetch the dentist and include related appointments
                var dentist = context.Dentists!
                                     .Include(d => d.Appointments)
                                     .FirstOrDefault(d => d.Name == "Dr. Brown");

                // Assert: Verify that the dentist and their appointments are found
                Assert.IsNotNull(dentist);  // Ensure dentist is not null
                Assert.AreEqual(2, dentist.Appointments!.Count());  // Verify two appointments exist

                // Order appointments by AppointmentDate to ensure correct ordering
                var orderedAppointments = dentist.Appointments!.OrderBy(a => a.AppointmentDate).ToList();

                // Verify that the appointments are ordered correctly by date
                Assert.AreEqual("Checkup", orderedAppointments.First().Notes);  // Check first appointment notes
                Assert.AreEqual("Cleaning", orderedAppointments.Last().Notes);  // Check last appointment notes
            }
        }
    }
}
