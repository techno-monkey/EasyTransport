using AutoMapper;
using ET.Models.DtoModels.Bus;
using ET.Trans.Bus.Interface;
using ET.Trans.Bus.RepoService;
using System.Linq;

namespace ET.Trans.Bus.Service
{
    public class BusService : IBusService
    {
        private readonly IBusRepo _busRepo;
        private readonly ITransporterRepo _transporterRepo;
        private readonly IMapper _mapper;
        public BusService(IBusRepo BusRepo,  IMapper mapper, ITransporterRepo transporterRepo)
        {
            _busRepo = BusRepo;
            _mapper = mapper;
            _transporterRepo = transporterRepo;
        }
        public async Task<IEnumerable<BusDto>> Get()
        {
            var response = _busRepo.GetAll();
            var mappingObjectBus = _mapper.Map<IEnumerable<BusDto>>(response);
            foreach (var item in mappingObjectBus)
            {
                var transp = _transporterRepo.Get(item.TransporterId);
                item.Name = transp.Name;
            }
            return mappingObjectBus;
        }

        public async Task<BusDto> Get(Guid id)
        {
            var response = _busRepo.Get(id);
            var mappingObjectBus = _mapper.Map<BusDto>(response);
            var transp = _transporterRepo.Get(mappingObjectBus.TransporterId);
            mappingObjectBus.Name = transp.Name;
            return mappingObjectBus;
        }

        public async Task<BusDto> Create(ET.Models.DataBase.Transport.Bus.Bus obj)
        {
            obj.Id = Guid.NewGuid();
            _busRepo.Create(obj);
            _busRepo.SaveChanges();
            return _mapper.Map<BusDto>(obj);
        }
    }
}
