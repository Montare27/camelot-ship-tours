namespace Api.Controllers
{
    using Application.Interfaces;
    using AutoMapper;
    using Domain.Models.Ship;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using ViewModels.Ship;

    [Authorize(Policy = "Business")]
    [ApiController]
    [Route("api/[controller]/")]
    [ApiExplorerSettings(GroupName = "Ships")]
    public class ShipController : ControllerBase
    {
        private readonly ICamelotDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<ShipController> _logger;
        private readonly IMemoryCache _memoryCache;

        public ShipController(IMapper mapper, ICamelotDbContext db, ILogger<ShipController> logger, IMemoryCache memoryCache)
        {
            _db = db;
            _logger = logger;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("all")]
        public async Task<ActionResult<List<GetShipShortlyViewModel>>> GetAll()
        {
            var ships = new List<GetShipShortlyViewModel>();
            try
            {
                ships = _mapper.Map<List<GetShipShortlyViewModel>>(await _db.Ships.ToListAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + " " + User.Identity?.Name);
                return BadRequest(ships);
            }
            
            return Ok(ships);
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("{id:int}")]
        public async Task<ActionResult<GetShipDetailsViewModel>> GetDetails(int id)
        {
            if (!_memoryCache.TryGetValue(id, out Ship? ship))
            {
                ship = await _db.Ships.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id == id);
                if (ship == null)
                {
                    return NotFound($"Ship was not found with id {id}.");
                }
            }
            var result = _mapper.Map<GetShipDetailsViewModel>(ship);
            _memoryCache.Set(ship!.Id, result, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            return Ok(result);
        }
        

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("category/{id:int}")]
        public async Task<ActionResult<GetShipShortlyViewModel>> GetShipsByCategory(int id)
        {
            var category = await _db.Categories
                .Include(category=>category.Ships!)
                .FirstOrDefaultAsync(category=>category.Id == id);
            
            if (category is not null)
            {
                var ships = category.Ships?.ToList();
                if (ships is not null)
                {
                    return Ok(_mapper.Map<List<GetShipDetailsViewModel>>(ships));
                }

                return NotFound("Any ship with selected category was not found.");
            }

            return NotFound("Category was not found.");
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("create")]
        public async Task<ActionResult<int>> CreateShip([FromBody] CreateShipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ship = _mapper.Map<Ship>(model);
                if (ship is not null)
                {
                    var category = await _db.Categories.FindAsync(model.CategoryId);
                    if (category is not null)
                    {
                        ship.Category = category;
                        await _db.Ships.AddAsync(ship);
                        await _db.SaveChangesAsync(CancellationToken.None);
                        return Ok(ship.Id);
                    }
                    
                    return NotFound("Category was not found!");

                }
                
                return NotFound("Ship was not found!");
            }

            foreach (var entry in ModelState.Values)
            {
                foreach (var error in entry.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }
            
            return BadRequest();
        }

        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GetShipShortlyViewModel>> EditShip(int id, [FromBody] EditShipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ship = await _db.Ships.FindAsync(id);
                if (ship is not null)
                {
                    var edited = _mapper.Map<Ship>(model);
                    var category = await _db.Categories.FindAsync(model.CategoryId);
                    if (category is null)
                    {
                        return NotFound("Category was not found.");
                    }
                    
                    if (edited is null)
                    {
                        return BadRequest("Ship's data is incorrect.");
                    }
                    
                    edited.Category = category;
                    _db.Ships.Entry(ship).CurrentValues.SetValues(edited);
                    await _db.SaveChangesAsync(CancellationToken.None);
                    return Ok(_mapper.Map<GetShipShortlyViewModel>(ship));
                }
                
                return NotFound("Ship with current Id was not found.");
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("delete/{id:int}")]
        public async Task<ActionResult> DeleteShip(int id)
        {
            if (ModelState.IsValid)
            {
                var ship = await _db.Ships.FindAsync(id);
                if(ship is not null)
                {
                    _db.Ships.Remove(ship);
                    await _db.SaveChangesAsync(CancellationToken.None);
                    return Ok();
                }

                return NotFound("Ship was not found");
            }
            
            foreach (var entry in ModelState.Values)
            {
                foreach (var error in entry.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }
            
            return BadRequest();
        }
    }   
}
