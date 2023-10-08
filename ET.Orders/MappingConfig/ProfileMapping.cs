using AutoMapper;
using ET.Models.DataBase.Order;
using ET.Models.DtoModels.Order;

namespace ET.Orders.MappingConfig
{
    public class ProfileMapping: Profile
    {
        public ProfileMapping()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
