using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourApp.Tests
{
    /// <summary>
    /// Test class for testing the functionality of the AppointmentsController.
    /// </summary>
    [TestClass]
    public class AppointmentsControllerTests
    {
        private AppointmentsController? _controller;
        private AppDbContext? _context;

        /// <summary>
        /// Initializes the in-memory database and controller for each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            // Use a unique database name for each test to avoid conflicts between tests
            var databaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            // Initialize in-memory database context
            _context = new AppDbContext(options);

            // Add some mock data to the database
            _context.Dentists!.AddRange(new List<Dentist>
            {
                new Dentist { Id = 1, Name = "Dr. John Doe" },
                new Dentist { Id = 2, Name = "Dr. Jane Smith" }
            });

            _context.Patients!.AddRange(new List<Patient>
            {
                new Patient { Id = 1, Name = "Patient A" },
                new Patient { Id = 2, Name = "Patient B" }
            });

            _context.SaveChanges();

            // Initialize the controller with the in-memory database context
            _controller = new AppointmentsController(_context);
        }

        /// <summary>
        /// Tests that the GetAppointments method returns an Ok result containing a list of appointments.
        /// </summary>
        [TestMethod]
        public async Task GetAppointments_ReturnsOkResult_WithListOfAppointments()
        {
            // Arrange
            var appointment = new Appointment
            {
                DentistId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now.AddHours(1) // setting an appointment time 1 hour ahead
            };
            _context!.Appointments!.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller!.GetAppointments();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult); // Ensure the result is OkObjectResult
            var returnValue = okResult.Value as List<Appointment>;
            Assert.IsNotNull(returnValue); // Ensure that the return value is a list of appointments
            Assert.AreEqual(1, returnValue.Count); // Ensure the list contains one appointment
        }

        /// <summary>
        /// Tests that the ScheduleAppointment method returns a CreatedAtActionResult when a valid appointment is scheduled.
        /// </summary>
        [TestMethod]
        public async Task ScheduleAppointment_ReturnsCreatedAtActionResult_WhenValidAppointmentIsScheduled()
        {
            // Arrange
            var appointment = new Appointment
            {
                DentistId = 2,
                PatientId = 2,
                AppointmentDate = DateTime.Now.AddHours(2) // setting an appointment time 2 hours ahead
            };

            // Act
            var result = await _controller!.ScheduleAppointment(appointment);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult); // Ensure the result is CreatedAtActionResult
            Assert.AreEqual(201, createdResult.StatusCode); // Ensure the status code is 201 (Created)

            var returnValue = createdResult.Value as Appointment;
            Assert.IsNotNull(returnValue); // Ensure the return value is an Appointment object
            Assert.AreEqual(appointment.DentistId, returnValue.DentistId); // Check if DentistId matches
            Assert.AreEqual(appointment.PatientId, returnValue.PatientId); // Check if PatientId matches
        }

        /// <summary>
        /// Tests that the ScheduleAppointment method returns a BadRequestObjectResult when scheduling an appointment for a time that conflicts with an existing appointment.
        /// </summary>
        [TestMethod]
        public async Task ScheduleAppointment_ReturnsBadRequest_WhenDentistIsNotAvailableAtThatTime()
        {
            // Arrange
            var conflictTime = DateTime.Now.AddHours(3); // Use a fixed value for the conflicting time
            var existingAppointment = new Appointment
            {
                DentistId = 1,
                PatientId = 1,
                AppointmentDate = conflictTime
            };

            _context!.Appointments!.Add(existingAppointment);
            await _context.SaveChangesAsync(); // Save to the in-memory database

            // Ensure the appointment was saved
            var savedAppointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.DentistId == 1 && a.AppointmentDate == conflictTime);
            Assert.IsNotNull(savedAppointment); // Ensure the existing appointment is saved

            var newAppointment = new Appointment
            {
                DentistId = 1,
                PatientId = 2,
                AppointmentDate = conflictTime // Use the same fixed time as the existing one
            };

            // Act
            var result = await _controller!.ScheduleAppointment(newAppointment);

            // Debug: Output the result type
            Console.WriteLine(result.GetType().Name);  // Should print "BadRequestObjectResult"

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult); // Ensure the result is BadRequestObjectResult
            
            // Check that the status code is 400 (BadRequest)
            Assert.AreEqual(400, badRequestResult.StatusCode);

            // Check the message returned is the expected one
            Assert.AreEqual("Dentist is not available at this time.", badRequestResult.Value);
        }
    }
}
