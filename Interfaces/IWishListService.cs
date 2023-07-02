namespace Interfaces
{
    public interface IWishListService
    {
        void AppendToWishList(ITrip trip);
        void RemoveFromWishList(ITrip trip);
        void ClearFromWishList();
        IEnumerable<int>? GetAllFromWishList();
    }
}
