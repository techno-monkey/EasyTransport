using trans = ET.Models.DataBase.Transport.Bus;
using ET.Trans.Bus.DataBase;

namespace ET.Trans.Bus.RepoService
{
    public class BusRepo : IBusRepo
    {

        private readonly DatabaseContext _dbContext;
        public BusRepo(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(trans.Bus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
            _dbContext.Buses.Add(obj);
        }

        public trans.Bus Get(Guid id) => _dbContext.Buses.Where(c => c.Id == id).FirstOrDefault();

        public IEnumerable<trans.Bus> GetAll() => _dbContext.Buses;

        public bool SaveChanges() => _dbContext.SaveChanges() > 0;
    }
}
