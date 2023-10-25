using Microsoft.EntityFrameworkCore;
using TexCode.DatabaseContext;
using TexCode.Entities;
using TexCode.Services;

namespace TexCodeTests
{
    public class SKUServiceTests : IDisposable
    {
        private APIContext _context;
        private ISKUService _skuService;
        public SKUServiceTests()
        {
            var options = new DbContextOptionsBuilder<APIContext>()
                .UseInMemoryDatabase(databaseName: "TexInMemoryDatabase")
                .Options;

            _context = new APIContext(options);
            _context.Database.EnsureCreated();

            _skuService = new SKUService(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Fact]
        public async void CreateSKUAsync_Should_Create_SKU()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = "123456",
                Name = "Test SKU",
                // Set other properties
            };

            // Act
            await _skuService.CreateSKUAsync(sku);

            // Assert
            var retrievedSKU = await _context.SKUs.FirstOrDefaultAsync(s => s.JanCode == sku.JanCode);
            Assert.NotNull(retrievedSKU);
            Assert.Equal(sku.Name, retrievedSKU.Name);
        }

        [Fact]
        public async void GetSKUByJanCodeAsync_Should_Return_Sku()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = "123456",
                Name = "Test SKU",
                // Set other properties
            };
            await _context.SKUs.AddAsync(sku);
            await _context.SaveChangesAsync();

            // Act
            var retrievedSku = await _skuService.GetSKUByJanCodeAsync(sku.JanCode);

            // Assert
            Assert.NotNull(retrievedSku);
            Assert.Equal(sku.Name, retrievedSku.Name);
        }

        [Fact]
        public async void GetAllSKUsAsync_Should_Return_All_SKUs()
        {
            // Arrange
            var skus = new List<SKU>
        {
            new SKU { JanCode = "SKU1", Name = "Item 1" },
            new SKU { JanCode = "SKU2", Name = "Item 2" },
            new SKU { JanCode = "SKU3", Name = "Item 3" },
        };

            await _context.SKUs.AddRangeAsync(skus);
            await _context.SaveChangesAsync();

            // Act
            var allSKUs = await _skuService.GetAllSKUsAsync();

            // Assert
            Assert.NotNull(allSKUs);
            Assert.Equal(skus.Count, allSKUs.Count);
        }

        [Fact]
        public async void UpdateSKUAsync_Should_Update_Sku()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = "123456",
                Name = "Original Name",
                // Set other properties
            };
            await _context.SKUs.AddAsync(sku);
            await _context.SaveChangesAsync();

            var updatedSKU = new SKU
            {
                JanCode = "123456",
                Name = "Updated Name",
                // Set other properties
            };

            // Act
            await _skuService.UpdateSKUAsync(updatedSKU);

            // Assert
            var retrievedSKU = await _context.SKUs.FirstOrDefaultAsync(s => s.JanCode == sku.JanCode);
            Assert.NotNull(retrievedSKU);
            Assert.Equal(updatedSKU.Name, retrievedSKU.Name);
        }

        [Fact]
        public async void DeleteSKUAsync_Should_Delete_Sku()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = "123456",
                Name = "Test SKU",
                // Set other properties
            };
            await _context.SKUs.AddAsync(sku);
            await _context.SaveChangesAsync();

            // Act
            await _skuService.DeleteSKUAsync(sku.JanCode);

            // Assert
            var retrievedSKU = await _context.SKUs.FirstOrDefaultAsync(s => s.JanCode == sku.JanCode);
            Assert.Null(retrievedSKU);
        }

        [Fact]
        public async void UpdateSKUAsync_Should_Handle_NonExistent_SKU()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = "123456",
                Name = "Original Name",
                // Set other properties
            };

            // Act
            await _skuService.UpdateSKUAsync(sku);

            // Assert
            // Ensure no exceptions are thrown when updating a non-existent SKU
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
        }

        [Fact]
        public async void GetAllSKUsAsync_Should_Return_Empty_List_When_No_SKUs()
        {
            // Arrange
            // Database is empty

            // Act
            var allSKUs = await _skuService.GetAllSKUsAsync();

            // Assert
            Assert.NotNull(allSKUs);
            Assert.Empty(allSKUs);
        }

        [Fact]
        public async void CreateSKUAsync_Should_Throw_Exception_On_Duplicate_JanCode()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = "123456",
                Name = "Test SKU",
                // Set other properties
            };
            await _context.SKUs.AddAsync(sku);
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                await _skuService.CreateSKUAsync(sku); // Attempt to create a duplicate SKU
            });
        }

        [Fact]
        public async void UpdateSKUAsync_Should_Throw_Exception_On_Invalid_JanCode()
        {
            // Arrange
            var invalidJanCode = "InvalidCode";
            var updatedSKU = new SKU
            {
                JanCode = "123456",
                Name = "Updated Name",
                // Set other properties
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _skuService.UpdateSKUAsync(updatedSKU);
            });
        }

        [Fact]
        public async void DeleteSKUAsync_Should_Throw_Exception_On_Invalid_JanCode()
        {
            // Arrange
            var invalidJanCode = "InvalidCode";

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _skuService.DeleteSKUAsync(invalidJanCode);
            });
        }

        [Fact]
        public async void GetSKUByJanCodeAsync_Should_Handle_Case_Insensitive_Search()
        {
            // Arrange
            var sku = new SKU
            {
                JanCode = "CaseInsensitiveCode",
                Name = "Test SKU",
                // Set other properties
            };
            await _context.SKUs.AddAsync(sku);
            await _context.SaveChangesAsync();

            // Act
            var retrievedSku = await _skuService.GetSKUByJanCodeAsync("caseinsensitivecode");

            // Assert
            Assert.NotNull(retrievedSku);
        }
    }
}