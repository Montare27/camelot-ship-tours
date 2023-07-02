namespace Api.Controllers
{
    using Application.Interfaces;
    using AutoMapper;
    using Domain.Models.Location;
    using Domain.Models.Service;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ViewModels.Trip;

    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Trips")]
    public class TripController : ControllerBase
    {
        private readonly IWishListService _service;
        private readonly IMapper _mapper;
        private readonly ICamelotDbContext _db;

        public TripController(IWishListService service, ICamelotDbContext db, IMapper mapper)
        {
            _service = service;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<GetTripShortlyViewModel>>> GetALl()
        {
            var trips = await _db.Trips.ToListAsync();
            if (trips.Count > 0)
            {
                return Ok(_mapper.Map<List<GetTripShortlyViewModel>>(trips));                
            }

            return NotFound("No one trip was found.");
        }
        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GetTripShortlyViewModel>> GetById(int id)
        {
            var trip = await _db.Trips.FindAsync(id);
            if (trip is not null)
            {
                return Ok(_mapper.Map<GetTripShortlyViewModel>(trip));
            }

            return NotFound("Trip was not found.");
        }
        
        [HttpPost]
        [Route("createTrip")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateTrip([FromBody] CreateTripViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = _mapper.Map<Trip>(model);
                await _db.Trips.AddAsync(trip);
                await _db.SaveChangesAsync(CancellationToken.None);
                return Ok();
            }

            return BadRequest(model);
        }
        
        [HttpPost]
        [Route("addLocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddLocations([FromBody] AddLocationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = await _db.Trips.FindAsync(model.TripId);
                if (trip is not null)
                {
                    var locations = _mapper.Map<List<Location>>(model.Locations);
                    if (locations is not null)
                    {
                        trip.Locations = locations;
                        await _db.Locations.AddRangeAsync(locations);
                        await _db.SaveChangesAsync(CancellationToken.None);
                        return Ok();
                    }
                    
                    return BadRequest("Entered locations' data was incorrect");
                }

                return NotFound("Trip was not found.");
            }
            
            return BadRequest(model);
        }
        
        [HttpPost]
        [Route("addImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddImages([FromBody] AddImagesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = await _db.Trips.FindAsync(model.TripId);
                if (trip is not null)
                {
                    var images = _mapper.Map<List<Image>>(model.Images);
                    if (images is not null)
                    {
                        trip.Images = images;
                        await _db.Images.AddRangeAsync(images);
                        await _db.SaveChangesAsync(CancellationToken.None);
                        return Ok();
                    }
                    
                    return BadRequest("Entered images' data was incorrect");
                }

                return NotFound("Trip was not found.");
            }
            
            return BadRequest(model);
        }
        
        [HttpPost]
        [Route("addToWishList/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddToWishList(int id)
        {
            var trip = await _db.Trips.FindAsync(id);
            if (trip is not null)
            {
                _service.AppendToWishList(trip);
                return Ok();
            }

            return NotFound("Trip was not found.");
        }
        
        [HttpGet]
        [Route("getWishList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<int>> GetWishList()
        {
            var trips = _service.GetAllFromWishList();
            if (trips is not null)
            {
                return Ok(trips.ToList());
            }

            return NotFound("Not one trip in your wishlist(");
        }
    }
}
