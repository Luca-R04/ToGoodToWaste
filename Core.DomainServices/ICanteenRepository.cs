using Core.Domain;

namespace Core.DomainServices
{
    public interface ICanteenRepository
    {
        IEnumerable<Canteen> Canteens { get; }

        Canteen? CreateCanteen(Canteen canteen);
        Canteen? ReadCanteen(int canteenId);
        Canteen? UpdateCanteen(Canteen canteen);
        Canteen? DeleteCanteen(Canteen canteen);
    }
}