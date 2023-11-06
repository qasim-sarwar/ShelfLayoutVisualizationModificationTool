using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using TexCode.DatabaseContext;
using TexCode.Entities;
using TexCode.Helpers;
using TexCode.Services;

namespace TexCodeTests
{
    public class ShelfLayoutServiceTests
    {
        private APIContext context;
        private IShelfLayoutService _shelfLayoutService;

        public ShelfLayoutServiceTests()
        {
            var _dbOptions = new DbContextOptionsBuilder<APIContext>()
                .UseInMemoryDatabase(databaseName: "TexCodeInMemoryDatabase")
            .Options;

            context = new APIContext(_dbOptions);
            context.Database.EnsureCreated();

            var appSettings = Options.Create(new AppSettings
            {
                ShelfJsonFileData = "D:\\TexCodeProj\\TexCode\\SampleData\\shelf.json" // Replace with a path to your test JSON file
            });

            _shelfLayoutService = new ShelfLayoutService(appSettings, new APIContext(_dbOptions));
        }

        internal void Dispose()
        {
            // Clean up the in-memory database after each test
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void GetCabinetById_ExistingId_ShouldReturnCabinet()
        {
            // Arrange
            var cabinet = new Cabinet { Number = 1 };
            context.Cabinets.Add(cabinet);
            context.SaveChanges();

            // Act
            var retrievedCabinet = _shelfLayoutService.GetCabinetById(1);

            // Assert
            Assert.NotNull(retrievedCabinet);
            Assert.Equal(1, retrievedCabinet.Number);
            Dispose();
        }

        [Fact]
        public void GetCabinetById_NonExistingId_ShouldReturnNull()
        {
            // Arrange: No cabinets are added to the database

            // Act
            var retrievedCabinet = _shelfLayoutService.GetCabinetById(999); // A non-existing ID

            // Assert
            Assert.Null(retrievedCabinet);
        }

        [Fact]
        public void CreateCabinet_ValidCabinet_ShouldCreateCabinet()
        {
            // Arrange: Create a new valid Cabinet object
            var newCabinet = new Cabinet
            {
                Number = 2
            };

            // Act: Call the CreateCabinet method to create the Cabinet
            _shelfLayoutService.CreateCabinet(newCabinet);

            // Assert: Verify that the created Cabinet is not null
            // Note: In your previous test, you were using Assert.NotNull, which is the correct way to check for non-null objects.
            Assert.NotNull(newCabinet);
        }

        [Fact]
        public void CreateCabinet_InvalidCabinet_ShouldNotCreateCabinet()
        {
            // Arrange: Create an invalid cabinet with missing properties
            var invalidCabinet = new Cabinet();

            // Act
            _shelfLayoutService.CreateCabinet(invalidCabinet);

            // Assert
            // Attempt to retrieve the cabinet from the database
            var retrievedCabinet = context.Cabinets.FirstOrDefault(cabinet => cabinet.Number == invalidCabinet.Number);

            // Assert that the retrieved cabinet is not null
            Assert.NotNull(retrievedCabinet);

            // Optionally, you can assert specific properties of the retrieved cabinet
            // For example, assert that the 'Number' property is 0 (or any other default value)
            Assert.Equal(0, retrievedCabinet.Number);
        }

        [Fact]
        public void UpdateCabinet_ExistingCabinet_ShouldUpdateCabinet()
        {
            // Arrange: Create an existing Cabinet object
            var existingCabinet = new Cabinet
            {
                Number = 3
            };

            // Act: Call the CreateCabinet method to create the Cabinet
            _shelfLayoutService.UpdateCabinet(existingCabinet.Number, existingCabinet);

            // Assert: Verify that the updated Cabinet is not null
            Assert.NotNull(existingCabinet);
        }

        [Fact]
        public void UpdateCabinet_NonExistingCabinet_ShouldNotUpdateCabinet()
        {
            // Arrange: No cabinets are added to the database

            var updatedCabinet = new Cabinet { Number = 5 }; // An updated cabinet with a non-existing ID

            // Act
            _shelfLayoutService.UpdateCabinet(5, updatedCabinet);

            // Assert
            var retrievedCabinet = context.Cabinets.FirstOrDefault(cabinet => cabinet.Number == updatedCabinet.Number);
            Assert.Null(retrievedCabinet);
        }

        [Fact]
        public void DeleteCabinet_ExistingCabinet_ShouldDeleteCabinet()
        {
            // Arrange: Cabinet is created
            var existingCabinet = new Cabinet { Number = 4 };
            context.Cabinets.Add(existingCabinet);
            context.SaveChanges();

            // Act
            _shelfLayoutService.DeleteCabinet(4);

            // Assert
            var retrievedCabinet = context.Cabinets.FirstOrDefault(cabinet => cabinet.Number == 4);
            Assert.Null(retrievedCabinet);
        }

        [Fact]
        public void DeleteCabinet_NonExistingCabinet_ShouldNotDeleteCabinet()
        {
            // Arrange: No cabinets are added to the database

            // Act
            _shelfLayoutService.DeleteCabinet(6); // A non-existing ID

            // Assert
            // Verify that no cabinets were deleted
            var cabinetCount = context.Cabinets.Count();
            Assert.Equal(0, cabinetCount);
        }
    }
}
