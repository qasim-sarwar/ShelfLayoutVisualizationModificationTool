using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TexCode.Entities
{
    public class ShelfLayout
    {
        public List<Cabinet> Cabinets { get; set; }
    }
    public class Cabinet
    {
        [Key]
        public int CabinetId { get; set; }
        public int Number { get; set; }
        public List<Row> Rows { get; set; }
        [NotMapped]
        public Position Position { get; set; }
        [NotMapped]
        public Size Size { get; set; }
    }
    public class Lane
    {
        [Key]
        public int LaneId { get; set; }
        public int Number { get; set; }

        public string JanCode { get; set; }

        public int Quantity { get; set; }

        public int PositionX { get; set; }
    }
    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }
    }
    public class Row
    {
        [Key]
        public int RowId { get; set; }
        public int Number { get; set; }

        [NotMapped]
        public List<Lane> Lanes { get; set; }

        public int PositionZ { get; set; }

        [NotMapped]
        public Size Size { get; set; }
    }
    public class Size
    {
        public int Width { get; set; }

        public int Depth { get; set; }

        public int Height { get; set; }
    }
}
