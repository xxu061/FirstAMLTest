using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAML.Domain
{
    public class Order
    {
        public IList<Parcel> Parcels { get; set; }
        public decimal Cost { get; set; }

    }
}
