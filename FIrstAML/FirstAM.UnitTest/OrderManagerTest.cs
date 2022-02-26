using FIrstAML.Lib;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FirstAM.UnitTest
{
    public class OrderManagerTest
    {
        [Fact]
        public async Task ShouldGetOrder()
        {
            Mock<IParcelPriceCalculator> calculator = new Mock<IParcelPriceCalculator>();
            OrderManager manager = new OrderManager(calculator.Object);

            Func<Task> act = async () =>
            {
                await manager.GetOrder(null);
            };

            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}
