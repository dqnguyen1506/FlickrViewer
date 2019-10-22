using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IBusinessLayer
    {
        #region Standard
        IEnumerable<Standard> GetAllStandards();

        Standard GetStandardByID(int id);

        Standard GetStandardByName(string name);

        void AddStandard(Standard standard);

        void UpdateStandard(Standard standard);

        void RemoveStandard(Standard standard);
        #endregion

        #region Student
        IEnumerable<Student> GetAllStudents();
        Student GetStudentByID(int id);
        void AddStudent(Student stu);
        void UpdateStudent(Student stu);
        void RemoveStudent(Student stu);

        IEnumerable<Teacher> GetAllTeacher();
        Teacher GetTeacherByID(int id);
        void AddTeacher(Teacher stu);
        void UpdateTeacher(Teacher stu);
        void RemoveTeacher(Teacher stu);
        #endregion
    }
}