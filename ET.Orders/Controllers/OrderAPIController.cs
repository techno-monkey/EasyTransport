using ET.Models.DataBase.Order;
using ET.Models.DtoModels.Order;
using ET.Orders.Interface;
using ET.Orders.Model;
using ET.Orders.ServiceBus;
using ET.ServiceBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ET.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IEventBusServiceBus _orderEventService;
        public OrderAPIController(IOrderService orderService, IEventBusServiceBus orderEventService)
        {
            _orderService = orderService;
            _orderEventService = orderEventService;
        }

        // GET: api/<OrderAPIController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<OrderDto>> Get()
        {
            return await _orderService.Get();
        }

        // GET api/<OrderAPIController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<OrderDto> Get(Guid id)
        {
            return await _orderService.Get(id);
        }

        // POST api/<OrderAPIController>
        //[HttpPost]
        //[Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<OrderDto> Post(Order obj)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException("Bus");
        //    var userId = User.FindFirst("Name")?.Value;
        //    obj.CreatedBy = userId;
        //    return await _orderService.Create(obj);
        //}

        [HttpPost]
        public async Task Post(OrderDto orderDto)
        {
            var events = new OrderIntegrationData(orderDto);
            _orderEventService.Publish(events);
        }

    }
}
