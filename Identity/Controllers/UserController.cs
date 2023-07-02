namespace Identity.Controllers
{
    using AutoMapper;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;


    [ApiController]
    [Authorize(Policy = "Moderators")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AuthDbContext _db;
        private readonly IMapper _mapper;
    
        public UserController(AuthDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
    
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<List<UserViewModel>> GetAll()
        {
            return _mapper.Map<List<UserViewModel>>(await _db.Users.ToListAsync());
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<UserViewModel> GetById(Guid id)
        {
            return _mapper.Map<UserViewModel>(await _db.Users.FindAsync(id));
        }
    }
}
