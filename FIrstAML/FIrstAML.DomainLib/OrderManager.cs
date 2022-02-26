using FirstAML.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIrstAML.Lib
{
    public class OrderManager: IOrderManager
    {
        private readonly IParcelPriceCalculator _parcelCalculator;
        public OrderManager(IParcelPriceCalculator parcelCalculator)
        {
            _parcelCalculator = parcelCalculator;
        }
        public Order GetOrder(IList<Parcel> items)
        {
            if (items == null || !items.Any())
            {
                throw new NullReferenceException();
            }

            Order order = new Order() { Parcels = new List<Parcel>() };
            foreach(var item in items)
            {
                order.Parcels.Add(_parcelCalculator.HydrateParcelItem(item));
            }

            order.Cost = order.Parcels.Sum(p => p.TotalPrice);

            return order;
        }
    }
}
