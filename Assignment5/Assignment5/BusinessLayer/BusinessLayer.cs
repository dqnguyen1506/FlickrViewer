using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IStandardRepository _standardRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        public BusinessLayer()
        {
            _standardRepository = new StandardRepository();
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
        }

        #region Standard
        public IEnumerable<Standard> GetAllStandards()
        {
            return _standardRepository.GetAll();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public Standard GetStandardByID(int id)
        {
            return _standardRepository.GetById(id);
        }

        public Student GetStudentByID(int id)
        {
            return _studentRepository.GetById(id);
        }

        public Standard GetStandardByName(string name)
        {
            return _standardRepository.GetSingle(
                s => s.StandardName.Equals(name),
                s => s.Students);
        }

        public void AddStandard(Standard standard)
        {
            _standardRepository.Insert(standard);
        }

        public void AddStudent(Student stu)
        {
            _studentRepository.Insert(stu);
        }

        public void UpdateStandard(Standard standard)
        {
            _standardRepository.Update(standard);
        }

        public void UpdateStudent(Student stu)
        {
            _studentRepository.Update(stu);
        }

        public void RemoveStandard(Standard standard)
        {
            _standardRepository.Delete(standard);
        }

        public void RemoveStudent(Student stu)
        {
            _studentRepository.Delete(stu);
        }

        #endregion

        #region Student
        public IEnumerable<Teacher> GetAllTeacher()
        {
            return _teacherRepository.GetAll();
        }
        public Teacher GetTeacherByID(int id)
        {
            return _teacherRepository.GetById(id);
        }
        public void AddTeacher(Teacher t)
        {
            _teacherRepository.Insert(t);
        }
        public void UpdateTeacher(Teacher t)
        {
            _teacherRepository.Update(t);
        }
        public void RemoveTeacher(Teacher t)
        {
            _teacherRepository.Delete(t);
        }

        #endregion
    }
}