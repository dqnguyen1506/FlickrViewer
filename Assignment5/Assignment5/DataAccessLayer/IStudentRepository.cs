using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IStudentRepository : IRepository<Student>
    {
       /* void Insert(Student entity);

        void Delete(Student entity);

        void Update(Student entity);

        Student GetById(int id);

        IQueryable<Student> SearchFor(Expression<Func<Student, bool>> predicate);

        IEnumerable<Student> GetAll();

        Student GetSingle(Func<Student, bool> where, params Expression<Func<Student, object>>[] navigationProperties);*/
    }
}
