using FirstAML.Domain;
using System.Threading.Tasks;

namespace FIrstAML.Lib
{
    public interface IParcelPriceCalculator
    {
        Task<Parcel> HydrateParcelItem(Parcel parcel);
    }
}