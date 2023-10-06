using ET.Models.DataBase.Transport.Bus;
using ET.Models.DtoModels.Bus;
using ET.Trans.Bus.Interface;
using ET.Trans.Bus.RepoService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ET.Trans.Bus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransporterAPIController : ControllerBase
    {

        private readonly ITransporterService _transporterService;
        public TransporterAPIController(ITransporterService transporterService)
        {
            _transporterService = transporterService;
        }

        // GET: api/<TransporterAPIController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TransporterDto>> Get()
        {
            return await _transporterService.Get();
        }

        // GET api/<TransporterAPIController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<TransporterDto> Get(Guid id)
        {
            return await _transporterService.Get(id);
        }

        // POST api/<TransporterAPIController>
        [HttpPost]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<TransporterDto> Post(Transporter obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Transporter");
            var userId = User.FindFirst("Name")?.Value;
            obj.CreatedBy = userId;
            return await _transporterService.Create(obj);
        }
    }
}
