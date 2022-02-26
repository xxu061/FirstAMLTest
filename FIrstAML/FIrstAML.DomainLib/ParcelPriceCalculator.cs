using FirstAML.Domain;
using Microsoft.Extensions.Configuration;
using System;

namespace FIrstAML.Lib
{
    public class ParcelPriceCalculator
    {
        private readonly IConfiguration _config;
        public ParcelPriceCalculator(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException();
        }

        public Parcel ProcessParcel(Parcel parcel)
        {
            return parcel;
        }
    }
}
