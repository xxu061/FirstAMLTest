using FirstAML.Domain;

namespace FIrstAML.Lib
{
    public interface IParcelPriceCalculator
    {
        Parcel ProcessParcel(Parcel parcel);
    }
}