using FirstAML.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIrstAML.Lib
{
    public class OrderManager: IOrderManager
    {
        private readonly IParcelPriceCalculator _parcelCalculator;
        public OrderManager(IParcelPriceCalculator parcelCalculator)
        {
            _parcelCalculator = parcelCalculator;
        }
        public async Task<Order> GetOrder(IList<Parcel> items)
        {
            if (items == null || !items.Any())
            {
                throw new NullReferenceException();
            }

            Order order = new Order() { Parcels = new List<Parcel>() };

            var tasks = new List<Task<Parcel>>();
            foreach(var item in items)
            {
                tasks.Add(_parcelCalculator.HydrateParcelItem(item));
            }

            order.Parcels = await Task.WhenAll(tasks);

            order.Cost = order.Parcels.Sum(p => p.TotalPrice);

            return order;
        }
    }
}
