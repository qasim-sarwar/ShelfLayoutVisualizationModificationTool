using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TexCode.DatabaseContext;
using TexCode.Entities;
using TexCode.Helpers;

namespace TexCode.Services
{
    public interface IShelfLayoutService
    {
        List<Cabinet> InitializeFromJsonFile();
        IEnumerable<Cabinet> GetCabinets();
        Cabinet GetCabinetById(int id);
        void CreateCabinet(Cabinet cabinet);
        void UpdateCabinet(int id, Cabinet updatedCabinet);
        void DeleteCabinet(int id);
    }
    public class ShelfLayoutService : IShelfLayoutService
    {
        private readonly AppSettings _appSettings;
        private readonly APIContext _context;
        public ShelfLayoutService(IOptions<AppSettings> appSettings, APIContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<Cabinet> InitializeFromJsonFile()
        {
            try
            {
                // Read the JSON data from the file
                string filePath = _appSettings.ShelfJsonFileData;
                string jsonText = File.ReadAllText(filePath);

                // Deserialize the JSON data into a ShelfLayoutData object
                var shelfLayoutData = JsonConvert.DeserializeObject<ShelfLayout>(jsonText);

                _context.Cabinets.AddRange(shelfLayoutData.Cabinets);
                _context.SaveChanges();

                // Return the list of initialized Cabinet objects
                return shelfLayoutData.Cabinets;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Return null in case of an error
                return null;
            }
        }
        public IEnumerable<Cabinet> GetCabinets()
        {
            return InitializeFromJsonFile();
        }
        public Cabinet GetCabinetById(int id)
        {
            return _context.Cabinets.AsNoTracking().FirstOrDefault(cabinet => cabinet.Number == id);
        }
        public void CreateCabinet(Cabinet cabinet)
        {
            if (cabinet == null)
            {
                throw new ArgumentNullException(nameof(cabinet));
            }
            _context.Cabinets.Add(cabinet);
            _context.SaveChanges();
        }
        public void UpdateCabinet(int id, Cabinet updatedCabinet)
        {
            if (updatedCabinet == null)
            {
                throw new ArgumentNullException(nameof(updatedCabinet));
            }
            var existingCabinet = _context.Cabinets.FirstOrDefault(cabinet => cabinet.Number == id);

            if (existingCabinet != null)
            {
                existingCabinet.Rows = updatedCabinet.Rows;
                existingCabinet.Size = updatedCabinet.Size;
                existingCabinet.Number = updatedCabinet.Number;
                existingCabinet.Position = updatedCabinet.Position;
                _context.Cabinets.Update(updatedCabinet);
                _context.SaveChanges();
            }
        }
        public void DeleteCabinet(int id)
        {
            var existingCabinet = _context.Cabinets.AsNoTracking().FirstOrDefault(cabinet => cabinet.Number == id);

            if (existingCabinet != null)
            {
                _context.Cabinets.Remove(existingCabinet);
                _context.SaveChanges();
            }
        }
    }
}