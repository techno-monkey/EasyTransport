using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Models.RepoService
{
    public interface IBaseRepoService<T> where T : class
    {
        bool SaveChanges();
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Create(T obj);
    }
}
