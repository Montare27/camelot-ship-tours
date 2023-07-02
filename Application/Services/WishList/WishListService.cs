namespace Application.Services.WishList
{
    using global::Interfaces;
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class WishListService  : IWishListService
    {
        private const string wishlist = "wishlist";

        private readonly IHttpContextAccessor _context;
        private readonly ISessionService _sessionService;
        private readonly ILogger<WishListService> _logger;
        private readonly ICamelotDbContext _db;
        
        public WishListService(ICamelotDbContext db, ILogger<WishListService> logger, ISessionService sessionService, IHttpContextAccessor context)
        {
            _db = db;
            _logger = logger;
            _sessionService = sessionService;
            _context = context;
        }

        public async void AppendToWishList(ITrip trip)
        {
            var existed = await _db.Trips.FirstOrDefaultAsync(x => x.Id == trip.Id);
            
            if (existed is not null)
            {
                var trips = _sessionService.GetItem<List<int>>(wishlist) ?? new List<int>();
                if (!trips.Contains(existed.Id))
                    trips.Add(existed.Id);
            
                _sessionService.SetItem<List<int>>(wishlist, trips);
            }
        }

        public async void RemoveFromWishList(ITrip trip)
        {
            var existed = await _db.Trips.Include(x => x.Ship)
                .FirstOrDefaultAsync(x => x.Id == trip.Id);
            
            if (existed is not null)
            {
                var trips = _sessionService.GetItem<List<int>>(wishlist) ?? new List<int>();
                trips.Remove(existed.Id);
                _context.HttpContext?.Session.Remove(wishlist);
                _sessionService.SetItem(wishlist, trips);
            }
        }

        public void ClearFromWishList()
        {
            if (_context.HttpContext?.Session.GetString(wishlist) is not null)
            {
                _context.HttpContext?.Session.Remove(wishlist);
            }
        }

        public IEnumerable<int>? GetAllFromWishList()
        {
            _logger.LogInformation("Entered to get function");
            return _sessionService.GetItem<List<int>>(wishlist);
        }
    }
}
