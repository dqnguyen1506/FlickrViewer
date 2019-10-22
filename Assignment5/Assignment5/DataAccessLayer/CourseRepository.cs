using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CourseRepository : Repository<Course>, ICourseRepo
    {
        private SchoolDBEntities courseEntities;
        public CourseRepository() : base(new SchoolDBEntities())
        {
            
        }

        public CourseRepository(SchoolDBEntities cCntx) : base(cCntx)
        {
            courseEntities = cCntx;
        }
    }
}
