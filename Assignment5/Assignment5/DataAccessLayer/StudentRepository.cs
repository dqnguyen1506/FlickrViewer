using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private SchoolDBEntities _studentEntities;

        public StudentRepository() : base(new SchoolDBEntities())
        {

        }

        public StudentRepository(SchoolDBEntities stuContext) : base (stuContext)
        {
            _studentEntities = stuContext;
        }

    }
}
