using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private SchoolDBEntities _context;
        public IStudentRepository Students;
        public IStandardRepository Standards;
        public ITeacherRepository Teachers;
        public ICourseRepo Courses;
        public UnitOfWork(SchoolDBEntities context)
        {
            _context = context;
            Students = new StudentRepository(_context);
            Standards = new StandardRepository(_context);
            Teachers = new TeacherRepository(_context);
            Courses = new CourseRepository(_context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
