using FirstAML.Domain;
using FIrstAML.Lib;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FirstAM.UnitTest
{
    public class OrderManagerTest
    {
        [Fact]
        public async Task ShouldThrowNullReferenceExceptionWhenNoParcel()
        {
            Mock<IParcelPriceCalculator> calculator = new Mock<IParcelPriceCalculator>();
            OrderManager manager = new OrderManager(calculator.Object);

            Func<Task> act = async () =>
            {
                await manager.GetOrder(null);
            };

            await act.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task ShouldInvokeHydrateItem()
        {
            Mock<IParcelPriceCalculator> calculator = new Mock<IParcelPriceCalculator>();
            calculator.Setup(c => c.HydrateParcelItem(It.IsAny<Parcel>())).ReturnsAsync(new Parcel());
            OrderManager manager = new OrderManager(calculator.Object);

            Func<Task> act = async () =>
            {
                await manager.GetOrder(new List<Parcel>() { new Parcel() });
            };

            await act();

            calculator.Verify(c => c.HydrateParcelItem(It.IsAny<Parcel>()), Times.Once);
        }
    }
}
