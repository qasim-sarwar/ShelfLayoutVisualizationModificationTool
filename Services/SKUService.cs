using Microsoft.EntityFrameworkCore;
using TexCode.DatabaseContext;
using TexCode.Entities;

namespace TexCode.Services
{
    public interface ISKUService
    {
        Task<SKU> CreateSKUAsync(SKU sku);
        Task<SKU> GetSKUByJanCodeAsync(string janCode);
        Task<List<SKU>> GetAllSKUsAsync();
        Task<SKU> UpdateSKUAsync(SKU updatedSKU);
        Task DeleteSKUAsync(string janCode);

    }
    public class SKUService : ISKUService
    {
        private readonly APIContext _context;

        public SKUService(APIContext context)
        {
            _context = context;
        }

        public async Task<SKU> CreateSKUAsync(SKU sku)
        {
            _context.SKUs.Add(sku);
            await _context.SaveChangesAsync();
            return sku;
        }

        public async Task<SKU> GetSKUByJanCodeAsync(string janCode)
        {
            return await _context.SKUs.FirstOrDefaultAsync(s => s.JanCode == janCode);
        }

        public async Task<SKU> UpdateSKUAsync(SKU updatedSKU)
        {
            var existingSKU = await _context.SKUs.FirstOrDefaultAsync(s => s.JanCode == updatedSKU.JanCode);
            if (existingSKU != null)
            {
                existingSKU.Name = updatedSKU.Name;
                existingSKU.X = updatedSKU.X;
                existingSKU.Y = updatedSKU.Y;
                existingSKU.Z = updatedSKU.Z;
                existingSKU.ImageURL = updatedSKU.ImageURL;
                existingSKU.Size = updatedSKU.Size;
                existingSKU.TimeStamp = updatedSKU.TimeStamp;
                existingSKU.Shape = updatedSKU.Shape;
                await _context.SaveChangesAsync();
            }
            return updatedSKU;
        }
        public async Task DeleteSKUAsync(string janCode)
        {
            var skuToDelete = await _context.SKUs.FirstOrDefaultAsync(s => s.JanCode == janCode);
            if (skuToDelete != null)
            {
                _context.SKUs.Remove(skuToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<SKU>> GetAllSKUsAsync()
        {
            return await _context.SKUs.ToListAsync();
        }
    }
}
