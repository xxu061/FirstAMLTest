using FirstAML.Domain;
using FIrstAML.Lib;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FirstAM.UnitTest
{
    public class ParcelCalculatorTest
    {
        [Fact]
        public async Task ShouldCalculateTotalPriceForParcel()
        {
            IConfiguration config = GetConfig();
            ParcelPriceCalculator calculator = new ParcelPriceCalculator(config);
            Parcel parcel = new Parcel() { Width = 20, Height = 20, Length = 20, Weight = 2 };

            var result = await calculator.HydrateParcelItem(parcel);

            result.TotalPrice.Should().Be(8);
        }

        [Fact]
        public async Task ShouldCalculateTotalPriceForHeavyParcel()
        {
            IConfiguration config = GetConfig();
            ParcelPriceCalculator calculator = new ParcelPriceCalculator(config);
            Parcel parcel = new Parcel() { Width = 20, Height = 20, Length = 20, Weight = 2, Heavy = true };

            var result = await calculator.HydrateParcelItem(parcel);

            result.TotalPrice.Should().Be(58);
        }

        [Fact]
        public async Task ShouldCalculateTotalPriceForSpeedyParcel()
        {
            IConfiguration config = GetConfig();
            ParcelPriceCalculator calculator = new ParcelPriceCalculator(config);
            Parcel parcel = new Parcel() { Width = 20, Height = 20, Length = 20, Weight = 2, Speedy = true };

            var result = await calculator.HydrateParcelItem(parcel);

            result.TotalPrice.Should().Be(16);
        }

        private IConfiguration GetConfig()
        {
            var dictionary = new Dictionary<string, string>() {
                {"MediumSizePrice", "8" },
                {"MediumWeightLimit", "3" }
            };
            return new ConfigurationBuilder()
                .AddInMemoryCollection(dictionary)
                .Build();
        }
    }
}
