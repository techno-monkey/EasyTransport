using ET.Models.DataBase.Transport.Bus;
using ET.Trans.Bus.DataBase;

namespace ET.Trans.Bus.RepoService
{
    public class TransporterRepo : ITransporterRepo
    {

        private readonly DatabaseContext _dbContext;
        public TransporterRepo(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Transporter obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
            _dbContext.Transporters.Add(obj);
        }

        public Transporter Get(Guid id) => _dbContext.Transporters.Where(c => c.Id == id).FirstOrDefault();

        public IEnumerable<Transporter> GetAll() => _dbContext.Transporters;

        public bool SaveChanges() => _dbContext.SaveChanges() > 0;
    }
}
