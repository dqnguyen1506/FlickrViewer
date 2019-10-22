using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment5
{
    class Program
    {

        /*    static BusinessLayer.BusinessLayer b1 = new BusinessLayer.BusinessLayer();
            AddMember();
            //break;
        */
       
        public static int InputSet(int low, int high)
        {
            String inp = "";
            int valIn = 101;
            
            while (valIn > 0)
            {

                //Console.WriteLine("Enter any integer from 1-100. Enter a negative when finished");
                inp = Console.ReadLine();
                while (inp.Equals("") || Regex.IsMatch(inp, @"^[a-zA-Z@#$%^&*!()_+={}]+$"))
                {
                    Console.WriteLine("Try again");
                    inp = Console.ReadLine();
                }

                valIn = Convert.ToInt32(inp);
                if (valIn > low && valIn < high)
                {
                    return valIn;
                }
                else if (valIn == -1)
                {
                    Console.WriteLine("Done.");                    
                }
                else if (valIn > high)
                {
                    Console.WriteLine("Invalid input try again");
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid input try again");
                    continue;
                }
            }
            return valIn;
        }


        public static void Main(string[] args)
        {
           /* IBusinessLayer bLayer = new BusinessLayer.BusinessLayer();

            Student s1 = bLayer.GetStudentByID(1);
            Console.WriteLine("StudentID: ", s1.StudentID, "\nStudent Name:", s1.StudentName);
            bLayer.RemoveStudent(s1);
            */

            //Console.ReadLine();
            using (var wrk = new UnitOfWork(new SchoolDBEntities()))
            {
                /*  var stu = (from sh in wrk.Students.GetAll()
                             where sh.StudentName == "Student1"
                             select sh).FirstOrDefault<Student>();
                  Student s2 = wrk.Students.GetById(2);
                  Console.WriteLine("Student ID: "+s2.StudentID+"\nStudent Name: "+ s2.StudentName);
                  Student s = new Student() { StudentID = 1234, StudentName = "Matthew" };

                  wrk.Students.Delete(s2);*/
                //s = wrk.Student.Where(s => s.StudentName == "New Student 1").FirstOrDefault<Student>();

                //bLayer.AddStudent(s);
                /*Table Teacher - Create, update, and delete - Update by seaching teacher id or teacher name
                 * Input the teacher id and then display all courses that has that teacher id.
                 * Display all standards
                 * Table Courses
                 * Create, update, and delete - Update by searching course id or course name.
                 * Display all courses
                 */
                bool flag = true;
                
                while (flag)
                {
                    Console.WriteLine("1 - AddTeacher" +
                                "\n2 - UpdateTeacher" +
                                "\n3 - DeleteTeacher" +
                                "\n4 - All Courses taught by a Teacher" +
                                "\n5 - AddCourse" +
                                "\n6 - UpdateCourse" +
                                "\n7 - DeleteCourse" +
                                "\n8 - Display Standards" +
                                "\n9 - Display Courses" +
                                "\n10 - Desplay Teachers" +
                                "\n-1 - Quit\n");

                    int c = InputSet(0, 12);
                    if (c == -1)
                    {
                        flag = false;
                        break;
                    }

                    else if (c == 1)
                    {

                        Console.WriteLine("Database before Add:\n");
                        Console.WriteLine(String.Format("Teacher ID" + "   " + "Teacher Name" + "    " + "Standard ID\n"));
                        foreach (var x in wrk.Teachers.GetAll())
                        {

                            Console.WriteLine(String.Format(x.TeacherId.ToString().PadRight(10) + "   " + x.TeacherName.PadRight(14) + "  " + x.StandardId));
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter Teacher Name ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Standard ID: ");
                        int sId = Convert.ToInt32(Console.ReadLine());

                        Teacher t1 = new Teacher() { TeacherName = name, StandardId = sId };

                        wrk.Teachers.Insert(t1);
                        wrk.Complete();

                        Console.WriteLine("Database after Add:\n");
                        Console.WriteLine(String.Format(/*"Teacher ID" + "   " + */
                "Teacher Name" + "    " + "Standard ID\n"));
                        foreach (var x in wrk.Teachers.GetAll())
                        {

                            Console.WriteLine(String.Format(/*x.TeacherId.ToString().PadRight(10) + "   " + */x.TeacherName.PadRight(14) + "  " + x.StandardId));
                        }
                        Console.WriteLine();


                    }
                    else if (c == 2)
                    {
                        Console.WriteLine(String.Format(/*"Teacher ID" + "   " + */"Teacher Name" + "    " + "Standard ID\n"));
                        foreach (var x in wrk.Teachers.GetAll())
                        {

                            Console.WriteLine(String.Format(/*x.TeacherId.ToString().PadRight(10) + "   " + */x.TeacherName.PadRight(14) + "  " + x.StandardId));
                        }
                        Console.WriteLine();
                         
                        Console.WriteLine("Enter Teacher ID ");
                        int id = InputSet(0, 20);
                        Teacher tmp = wrk.Teachers.GetById(id);
                        Console.WriteLine("Change Name or Skip");
                        string name = Console.ReadLine();
                        Console.WriteLine("Change Standard or Skip");
                        int sId = InputSet(0, 100);
                        Standard s = wrk.Standards.GetById(sId);
                        tmp.TeacherName = name;
                        //tmp.StandardId = sId;
                        tmp.Standard = s;

                        wrk.Teachers.Update(tmp);
                        wrk.Complete();

                        Console.WriteLine(String.Format(/*"Teacher ID" + "   " + */"Teacher Name" + "    " + "Standard ID\n"));
                        foreach (var x in wrk.Teachers.GetAll())
                        {

                            Console.WriteLine(String.Format(/*x.TeacherId.ToString().PadRight(10) + "   " + */x.TeacherName.PadRight(14) + "  " + x.StandardId));
                        }
                        Console.WriteLine();


                    }
                    else if (c == 3)
                    {
                        Console.WriteLine(String.Format("Teacher ID" + "   " + "Teacher Name" + "    " + "Standard ID\n"));
                        foreach (var x in wrk.Teachers.GetAll())
                        {

                            Console.WriteLine(String.Format(x.TeacherId.ToString().PadRight(10) + "   " + x.TeacherName.PadRight(14) + "  " + x.StandardId));
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter Teacher ID ");
                        int id = InputSet(1, 100);
                        Teacher tmp = wrk.Teachers.GetById(id);
                        //wrk.Teachers.SearchFor(i => i.TeacherId.Equals(id));
                        Console.WriteLine("Delete Teacher with ID: " + id);
                        wrk.Teachers.Delete(tmp);
                        wrk.Complete();
                        Console.WriteLine(String.Format("Teacher ID" + "   " + "Teacher Name" + "    " + "Standard ID\n"));
                        foreach (var x in wrk.Teachers.GetAll())
                        {

                            Console.WriteLine(String.Format(x.TeacherId.ToString().PadRight(10) + "   " + x.TeacherName.PadRight(14) + "  " + x.StandardId));
                        }
                        Console.WriteLine("\n");
                    }
                    else if (c == 4)
                    {
                        Console.WriteLine("Enter Teacher ID ");
                        int id = InputSet(1, 100);//wrk.Teachers.GetAll().Count());

                        Console.WriteLine(String.Format("Course ID".PadRight(10) + "Course Name".PadRight(5) + " Teacher ID"));
                        foreach (var x in wrk.Courses.GetAll())
                        {
                            if (x.TeacherId == id)
                            {
                                Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(10) + x.CourseName.PadRight(12) + x.TeacherId));
                            }

                        }
                        Teacher tmp = wrk.Teachers.GetById(id);
                    }
                    else if (c == 5)
                    {
                        Console.WriteLine("Database before Add:\n");
                        Console.WriteLine(String.Format("Course ID" + "   " + "Course Name" + "    " + "Teacher ID\n"));
                        foreach (var x in wrk.Courses.GetAll())
                        {
                            Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(9) + "   " + x.CourseName.PadRight(13) + "  " + x.TeacherId));
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter Course Name ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Teacher ID: ");
                        int tId = Convert.ToInt32(Console.ReadLine());

                        Course c1 = new Course() { CourseName = name, Teacher = wrk.Teachers.GetById(tId)};//, Teacher = wrk.Teachers.GetById(tId)};

                        wrk.Courses.Insert(c1);
                        wrk.Complete();

                        Console.WriteLine("Database after Add:\n");
                        Console.WriteLine(String.Format("Course ID" + "   " + "Course Name" + "    " + "Teacher ID\n"));
                        foreach (var x in wrk.Courses.GetAll())
                        {

                            Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(9) + "   " + x.CourseName.PadRight(13) + "  " + x.TeacherId));
                        }
                        Console.WriteLine();
                    }
                    else if (c == 6)
                    {
                        Console.WriteLine(String.Format("Course ID" + "   " + "Course Name" + "    " + "Teacher ID\n"));
                        foreach (var x in wrk.Courses.GetAll())
                        {

                            Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(9) + "   " + x.CourseName.PadRight(13) + "  " + x.TeacherId));
                        }
                        Console.WriteLine();
                        Console.WriteLine("Enter Course ID ");
                        int id = InputSet(1, wrk.Courses.GetAll().Count());
                        Course tmp = wrk.Courses.GetById(id);
                        Console.WriteLine("Change Name or Skip");
                        string name = Console.ReadLine();
                        Console.WriteLine("Change Teacher ID or Skip");
                        int sId = InputSet(0, 100);
                        Teacher t = wrk.Teachers.GetById(sId);
                        tmp.CourseName = name;
                        //tmp.StandardId = sId;
                        //tmp.TeacherId = sId;
                        tmp.Teacher = t;
                        //SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                        //SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                        wrk.Courses.Update(tmp);
                        wrk.Complete();

                        Console.WriteLine(String.Format("Course ID" + "   " + "Course Name" + "    " + "Teacher ID\n"));
                        foreach (var x in wrk.Courses.GetAll())
                        {

                            Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(9) + "   " + x.CourseName.PadRight(13) + "  " + x.TeacherId));
                        }


                    }
                    else if(c == 7)
                    {
                        Console.WriteLine(String.Format("Course ID" + "   " + "Course Name" + "    " + "Teacher ID\n"));
                        foreach (var x in wrk.Courses.GetAll())
                        {

                            Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(9) + "   " + x.CourseName.PadRight(13) + "  " + x.TeacherId));
                        }
                        Console.WriteLine();
                        Console.WriteLine("Enter Course ID ");
                        int id = InputSet(1, 100);
                        Course tmp = wrk.Courses.GetById(id);
                        //wrk.Teachers.SearchFor(i => i.TeacherId.Equals(id));
                        Console.WriteLine("Delete Course with ID: " + id);
                        wrk.Courses.Delete(tmp);
                        wrk.Complete();
                        Console.WriteLine(String.Format("Course ID" + "   " + "Course Name" + "    " + "Teacher ID\n"));
                        foreach (var x in wrk.Courses.GetAll())
                        {

                            Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(9) + "   " + x.CourseName.PadRight(13) + "  " + x.TeacherId));
                        }
                        Console.WriteLine();
                    }
                    else if (c == 8)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Standard ID" + "   " + "Standard Name" + "    " + "Description\n");
                        foreach (var x in wrk.Standards.GetAll())
                        {
                            
                            Console.WriteLine(x.StandardId.ToString().PadRight(13) + "  " + x.StandardName.PadRight(13) + "   " + x.Description);
                        }
                        Console.WriteLine();
                    }
                    else if (c == 9)
                    {
                        Console.WriteLine();
                        Console.WriteLine(String.Format("Course ID" + "   " + "Course Name" + "    " + "Teacher ID\n"));
                        foreach (var x in wrk.Courses.GetAll())
                        {
                            
                            Console.WriteLine(String.Format(x.CourseId.ToString().PadRight(9) + "   " + x.CourseName.PadRight(13)  + "  " + x.TeacherId));
                        }
                        Console.WriteLine();
                    }
                    else if(c == 10)
                    {
                        Console.WriteLine(String.Format("Teacher ID" + "   " + "Teacher Name" + "    " + "Standard ID\n"));
                        foreach (var x in wrk.Teachers.GetAll())
                        {

                            Console.WriteLine(String.Format(x.TeacherId.ToString().PadRight(10) + "   " + x.TeacherName.PadRight(14) + "  " + x.StandardId));
                        }
                        Console.WriteLine("\n");
                    }
                    

                }
                //Console.WriteLine("Delete Student 2");

                //Student s1 = new Student() { StudentName = "Matthew"};
                //Student s2 = wrk.Students.GetById(2);
                /* if (s2 == null)
                 {
                     Console.WriteLine("Nope");
                     s2 = new Student() { StudentName = "Student2" };
                 }
                 else
                 {
                     wrk.Students.Delete(s2);
                 }*/

                //wrk.Students.Insert(s1);

               // s1.StudentName = "Mikey";
                //wrk.Students.Update(s1);

              /*  foreach (Student x in wrk.Students.GetAll())
                {
                    
                    Console.WriteLine(x.StudentName + ":" + x.StudentID);
                }


                Console.ReadKey();*/
                //wrk.Standard.Insert(new Standard());
                
                wrk.Complete();
            }

        }
    }
}

       
