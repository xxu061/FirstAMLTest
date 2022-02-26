using System;

namespace FirstAML.Domain
{
    public class Parcel
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public ParcelSize Size { get; set; }
        public decimal Price { get; set; }
        public bool Speedy { get; set; }
        public decimal SpeedyCost { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
