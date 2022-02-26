using FirstAML.Domain;
using System.Collections.Generic;

namespace FIrstAML.Lib
{
    public interface IOrderManager
    {
        Order GetOrder(IList<Parcel> items);
    }
}