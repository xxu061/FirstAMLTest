using FirstAML.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FIrstAML.Lib
{
    public interface IOrderManager
    {
        Task<Order> GetOrder(IList<Parcel> items);
    }
}