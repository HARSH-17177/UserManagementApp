using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Repository
{
    public interface IRepository<TEntity,TIdentity>
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByCriteria(string filterCriteria);
        TEntity FindById(int id);
        void Upsert(TEntity entity);
        void RemoveById(TIdentity id);
    }
}
