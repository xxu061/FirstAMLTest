using FirstAML.Domain;

namespace FIrstAML.Lib
{
    public interface IParcelPriceCalculator
    {
        Parcel HydrateParcelItem(Parcel parcel);
    }
}