using ET.Models.DtoModels.Bus;
using ET.Trans.Bus.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trans = ET.Models.DataBase.Transport.Bus;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ET.Trans.Bus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusAPIController : ControllerBase
    {

        private readonly IBusService _busService;
        public BusAPIController(IBusService BusService)
        {
            _busService = BusService;
        }

        // GET: api/<BusAPIController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<BusDto>> Get()
        {
            return await _busService.Get();
        }

        // GET api/<BusAPIController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<BusDto> Get(Guid id)
        {
            return await _busService.Get(id);
        }

        // POST api/<BusAPIController>
        [HttpPost]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<BusDto> Post(trans.Bus obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Bus");
            var userId = User.FindFirst("Name")?.Value;
            obj.CreatedBy = userId;
            return await _busService.Create(obj);
        }
    }
}
