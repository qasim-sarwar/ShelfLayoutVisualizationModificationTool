using System.ComponentModel.DataAnnotations;

namespace TexCode.Entities
{
    public class SKU
    {
        [Key]
        public string JanCode { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string ImageURL { get; set; }
        public int Size { get; set; }
        public long TimeStamp { get; set; }
        public Shape Shape { get; set; }
    }
    public enum Shape
    {
        Bottle,
        Can
    }
}
