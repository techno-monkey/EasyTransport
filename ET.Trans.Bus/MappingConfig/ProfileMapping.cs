using AutoMapper;
using trans = ET.Models.DataBase.Transport.Bus;
using ET.Models.DtoModels.Bus;

namespace ET.Trans.Bus.MappingConfig
{
    public class ProfileMapping: Profile
    {
        public ProfileMapping()
        {
            CreateMap<trans.Transporter, TransporterDto>();
            CreateMap<trans.Bus, BusDto>();
        }
    }
}
