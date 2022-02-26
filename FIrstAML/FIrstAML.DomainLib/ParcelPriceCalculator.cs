using FirstAML.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace FIrstAML.Lib
{
    public class ParcelPriceCalculator : IParcelPriceCalculator
    {
        private readonly IConfiguration _config;
        /// <summary>
        /// Ideally these figures should from config
        /// </summary>
        private readonly int _smallSizeInCm = 10;
        private readonly int _mediumSizeInCm = 50;
        private readonly int _largeSizeInCm = 100;
        public ParcelPriceCalculator(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException();
        }

        public async Task<Parcel> HydrateParcelItem(Parcel parcel)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (!ValidateParcel(parcel))
                    {
                        //We should log more details about the parcel
                        throw new NullReferenceException("Invalid parcel payload");
                    }

                    parcel.Size = CalculateSize(parcel);
                    parcel.Price = CalculatePrice(parcel.Size, parcel.Weight, parcel.Heavy);
                    if (parcel.Speedy)
                    {
                        parcel.SpeedyCost = CalculateSpeedyCost(parcel);
                    }
                    parcel.TotalPrice = parcel.Price + (parcel.Speedy ? parcel.SpeedyCost : 0);

                    
                });

                return parcel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Assume measurements come in CM.
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
        private ParcelSize CalculateSize(Parcel parcel)
        {
            if (parcel.Height <= _smallSizeInCm && parcel.Length <= _smallSizeInCm && parcel.Width <= _smallSizeInCm)
            {
                return ParcelSize.Small;
            }
            else if (parcel.Height <= _mediumSizeInCm && parcel.Length <= _mediumSizeInCm && parcel.Width <= _mediumSizeInCm)
            {
                return ParcelSize.Medium;
            }
            else if (parcel.Height <= _largeSizeInCm && parcel.Length <= _largeSizeInCm && parcel.Width <= _largeSizeInCm)
            {
                return ParcelSize.Large;
            }
            else if (parcel.Height > _largeSizeInCm || parcel.Width > _largeSizeInCm || parcel.Length > _largeSizeInCm)
            {
                return ParcelSize.ExtraLarge;
            }
            else
            {
                throw new ArgumentException(string.Format("Cannot determine parcel size: height {0}, width {1}, length {2}",
                    parcel.Height, parcel.Width, parcel.Length));
            }
        }

        private decimal CalculatePrice(ParcelSize size, decimal weight, bool heavy)
        {
            decimal price = 0;
            decimal sizePrice = GetConfigValue(_config[string.Format("{0}SizePrice", size)]);
            decimal weightLimit = GetConfigValue(_config[string.Format("{0}WeightLimit", size)]);

            price += sizePrice;
            price += CalculateWeightPrice(weightLimit, weight, heavy);

            return price;
        }

        private decimal GetConfigValue(string value)
        {
            if (!string.IsNullOrEmpty(value) && decimal.TryParse(value, out decimal result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid config for " + value);
            }
        }

        private bool ValidateParcel(Parcel parcel)
        {
            return !(parcel == null || parcel.Height <= 0 || parcel.Width <= 0 || parcel.Length <= 0);
        }
        private decimal CalculateSpeedyCost(Parcel parcel)
        {
            return parcel.Price;
        }

        private decimal CalculateWeightPrice(decimal weightLimit, decimal weight, bool heavy)
        {
            int dollarPerKg = 2;
            if (heavy)
            {
                weightLimit = 50;
                dollarPerKg = 1;
            }

            if (weight > weightLimit)
            {
                return (weight - weightLimit) * dollarPerKg;
            }
            else
            {
                return 0;
            }

        }
    }
}
