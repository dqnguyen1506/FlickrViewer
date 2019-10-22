using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IStandardRepository : IRepository<Standard>
    {
        /*void Insert(Standard entity);

        void Delete(Standard entity);

        void Update(Standard entity);

        Standard GetById(int id);

        IQueryable<Standard> SearchFor(Expression<Func<Standard, bool>> predicate);

        IEnumerable<Standard> GetAll();

        Standard GetSingle(Func<Standard, bool> where, params Expression<Func<Standard, object>>[] navigationProperties);*/


    }
}