using AutoMapper;
using ET.Models.DataBase.Transport.Bus;
using ET.Models.DtoModels.Bus;
using ET.Trans.Bus.Interface;
using ET.Trans.Bus.RepoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ET.Trans.Bus.Service
{
    public class TransporterService : ITransporterService
    {
        private readonly ITransporterRepo _transporterRepo;
        private readonly IMapper _mapper;
        public TransporterService(ITransporterRepo transporterRepo, IMapper mapper)
        {
            _transporterRepo = transporterRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TransporterDto>> Get()
        {
            var response = _transporterRepo.GetAll();
            return _mapper.Map<IEnumerable<TransporterDto>>(response);
        }

        public async Task<TransporterDto> Get(Guid id)
        {
            var response = _transporterRepo.Get(id);
            return _mapper.Map<TransporterDto>(response);
        }

        public async Task<TransporterDto> Create(Transporter obj)
        {
            obj.Id = Guid.NewGuid();
            _transporterRepo.Create(obj);
            _transporterRepo.SaveChanges();
            return _mapper.Map<TransporterDto>(obj);
        }
    }
}
