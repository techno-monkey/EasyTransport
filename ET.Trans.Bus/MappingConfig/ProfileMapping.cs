using AutoMapper;
using ET.Models.DataBase.Transport.Bus;
using ET.Models.DtoModels.Bus;

namespace ET.Trans.Bus.MappingConfig
{
    public class ProfileMapping: Profile
    {
        public ProfileMapping()
        {
            CreateMap<Transporter, TransporterDto>();
        }
    }
}
