using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourApp.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="DentistsController"/>.
    /// </summary>
    [TestClass]
    public class DentistsControllerTests
    {
        private DentistsController? controller;
        private AppDbContext? context;

        /// <summary>
        /// Initializes the test environment by creating an in-memory database with mock data.
        /// This method is run before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            // Use a unique database name for each test
            var databaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            // Use the constructor of AppDbContext that accepts DbContextOptions
            context = new AppDbContext(options);

            // Add mock data to the in-memory database
            context.Dentists!.AddRange(new List<Dentist>
            {
                new Dentist { Id = 1, Name = "Dr. John Doe" },
                new Dentist { Id = 2, Name = "Dr. Jane Smith" }
            });
            context.SaveChanges();

            // Initialize the controller with the in-memory database context
            controller = new DentistsController(context);
        }

        /// <summary>
        /// Tests the GetDentists method to ensure it returns a list of dentists.
        /// </summary>
        /// <returns>A task that represents the asynchronous test operation.</returns>
        [TestMethod]
        public async Task GetDentists_Returns_OkResult_WithList_OfDentists()
        {
            // Act
            var result = await controller!.GetDentists();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);  // Ensure that the result is OkObjectResult
            var returnValue = okResult.Value as List<Dentist>;
            Assert.IsNotNull(returnValue);  // Ensure the return value is a list of dentists
            Assert.AreEqual(2, returnValue.Count);  // Ensure there are two dentists in the list
        }

        /// <summary>
        /// Tests the AddDentist method to ensure it correctly creates a new dentist when a valid dentist is provided.
        /// </summary>
        /// <returns>A task that represents the asynchronous test operation.</returns>
        [TestMethod]
        public async Task AddDentist_Returns_Created_AtAction_Result_When_Valid_Dentist_IsAdded() 
        {
            // Arrange
            var dentist = new Dentist
            {
                Name = "Dr. Alice Johnson"
            };

            // Act
            var result = await controller!.AddDentist(dentist);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);  // Ensure that the result is CreatedAtActionResult

            // Check the status code returned (201 Created)
            Assert.AreEqual(201, createdResult.StatusCode);

            // Check that the response includes the dentist object
            var returnValue = createdResult.Value as Dentist;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(dentist.Name, returnValue.Name);  // Ensure that the dentist's name is the same

            // Ensure that the dentist has been assigned an Id (indicating it's saved)
            Assert.AreNotEqual(0, returnValue.Id);  
        }
    }
}
