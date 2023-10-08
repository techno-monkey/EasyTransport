using AutoMapper;
using ET.Models.DataBase.Order;
using ET.Models.DtoModels.Order;
using ET.Orders.Interface;
using ET.Orders.RepoService;

namespace ET.Orders.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepo orderRepo,  IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDto>> Get()
        {
            var response = _orderRepo.GetAll();
            var mappingObjectBus = _mapper.Map<IEnumerable<OrderDto>>(response);
            
            return mappingObjectBus;
        }

        public async Task<OrderDto> Get(Guid id)
        {
            var response = _orderRepo.Get(id);
            var mappingObjectBus = _mapper.Map<OrderDto>(response);
            return mappingObjectBus;
        }

        public async Task<OrderDto> Create(Order obj)
        {
            obj.OrderId = Guid.NewGuid();
            _orderRepo.Create(obj);
            _orderRepo.SaveChanges();
            return _mapper.Map<OrderDto>(obj);
        }
    }
}
