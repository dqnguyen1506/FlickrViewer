using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        private SchoolDBEntities _teachers;
        public TeacherRepository() : base(new SchoolDBEntities())
        {

        }

        public TeacherRepository(SchoolDBEntities tCntx) : base(tCntx)
        {
            _teachers = tCntx;
        }
    }


}
