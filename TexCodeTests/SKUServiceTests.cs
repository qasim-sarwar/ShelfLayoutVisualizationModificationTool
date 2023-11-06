using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TexCode.DatabaseContext;
using TexCode.Entities;
using TexCode.Services;

namespace TexCodeTests
{
    public class SKUServiceTests
    {
        private APIContext _context;
        private ISKUService _skuService;
        private static readonly Random random = new Random();
        private const string Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string GenerateRandomAlphanumericString(int length = 10)
        {
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(Characters.Length);
                sb.Append(Characters[index]);
            }
            return sb.ToString();
        }
        public SKUServiceTests()
        {
            var options = new DbContextOptionsBuilder<APIContext>()
                .UseInMemoryDatabase(databaseName: "TexCodeInMemoryDatabase")
                .Options;

            _context = new APIContext(options);
            _context.Database.EnsureCreated();

            _skuService = new SKUService(_context);
        }

        internal void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async void CreateSKUAsync_Should_Create_SKU()
        {
            // Arrange: Create a new SKU object with valid data
            var newSKU = new SKU
            {
                JanCode = "123456789",
                Name = "Sample SKU",
                // Set other required properties here
                ImageURL = "https://example.com/image.jpg" // Provide a valid ImageURL
            };

            // Act: Call the CreateSKUAsync method to create the SKU
            var createdSKU = await _skuService.CreateSKUAsync(newSKU);

            // Assert: Verify that the createdSKU is not null
            Assert.NotNull(createdSKU);

            // Optionally, you can assert specific properties of the created SKU here
            Assert.Equal(newSKU.JanCode, createdSKU.JanCode);
            Assert.Equal(newSKU.Name, createdSKU.Name);
        }

        [Fact]
        public async void GetSKUByJanCodeAsync_Should_Return_Sku()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = GenerateRandomAlphanumericString(),
                Name = "Test SKU",
                ImageURL = "https://example.com/image.jpg"
                // Set other properties
            };
            await _context.SKUs.AddAsync(sku);
            await _context.SaveChangesAsync();

            // Act
            var retrievedSku = await _skuService.GetSKUByJanCodeAsync(sku.JanCode);

            // Assert
            Assert.NotNull(retrievedSku);
            Assert.Equal(sku.Name, retrievedSku.Name);
            Dispose();
        }

        [Fact]
        public async void GetAllSKUsAsync_Should_Return_All_SKUs()
        {
            // Arrange
            var skus = new List<SKU>
        {
            new SKU { JanCode = GenerateRandomAlphanumericString(), Name = "Item 1", ImageURL = "https://example.com/image.jpg" },
            new SKU { JanCode = GenerateRandomAlphanumericString(), Name = "Item 2", ImageURL = "https://example.com/image1.jpg" },
            new SKU { JanCode = GenerateRandomAlphanumericString(), Name = "Item 3", ImageURL = "https://example.com/image2.jpg" },
        };

            await _context.SKUs.AddRangeAsync(skus);
            await _context.SaveChangesAsync();

            // Act
            var allSKUs = await _skuService.GetAllSKUsAsync();

            // Assert
            Assert.NotNull(allSKUs);
            Assert.Equal(skus.Count, allSKUs.Count);
            Dispose();
        }

        [Fact]
        public async void UpdateSKUAsync_Should_Update_Sku()
        {
            // Arrange: Create an existing SKU object
            var existingSKU = new SKU
            {
                JanCode = "12345",
                Name = "Product A",
                // Set other properties as needed
            };

            // Act: Call the UpdateSKUAsync method to update the SKU
            var updatedSKU = await _skuService.UpdateSKUAsync(existingSKU);

            // Assert: Verify that the updated SKU is not null
            Assert.NotNull(updatedSKU);
        }

        [Fact]
        public async void UpdateSKUAsync_Should_Handle_NonExistent_SKU()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = GenerateRandomAlphanumericString(),
                Name = "Original Name",
                ImageURL = "https://example.com/image.jpg"
                // Set other properties
            };

            // Act
            await _skuService.UpdateSKUAsync(sku);

            // Assert
            // Ensure no exceptions are thrown when updating a non-existent SKU
            Dispose();
        }

        [Fact]
        public async void DeleteSKUAsync_Should_Handle_NonExistent_SKU()
        {
            // Arrange
            var janCode = "NonExistentCode";

            // Act
            await _skuService.DeleteSKUAsync(janCode);

            // Assert
            // Ensure no exceptions are thrown when deleting a non-existent SKU
            Dispose();
        }

        [Fact]
        public async void GetSKUByJanCodeAsync_Should_Return_Null_When_Not_Found()
        {
            // Arrange
            var janCode = "NonExistentCode";

            // Act
            var retrievedSku = await _skuService.GetSKUByJanCodeAsync(janCode);

            // Assert
            Assert.Null(retrievedSku);
            Dispose();
        }
    }
}