
using ET.Models.DataBase.Order;
using ET.Orders.DataBase;

namespace ET.Orders.RepoService
{
    public class OrderRepo : IOrderRepo
    {

        private readonly DatabaseContext _dbContext;
        public OrderRepo(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Order obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
            _dbContext.Orders.Add(obj);
        }

        public Order Get(Guid id) => _dbContext.Orders.Where(c => c.OrderId == id).FirstOrDefault();

        public IEnumerable<Order> GetAll() => _dbContext.Orders;

        public bool SaveChanges() => _dbContext.SaveChanges() > 0;
    }
}
