using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exem
{
    class GradesDAO
    {
        public static void PrintGrades()
        {
            using (Exem5Entities entities = new Exem5Entities())
            {
                entities.Grades.Join(entities.Students,
                                                g => g.STUDENT_ID,
                                                s => s.ID,
                                                (g, s) => new 
                                                {
                                                    Student_name = s.FIRST_NAME,
                                                    COURSE_ID = g.COURSE_ID,
                                                    GRADE1 = g.GRADE1
                                                }).Join(entities.Courses,
                                                g => g.COURSE_ID,
                                                c => c.ID,
                                                (g, c) => new 
                                                {
                                                    Student_name = g.Student_name,
                                                    Course_name = c.NAME,
                                                    GRADE1 = g.GRADE1
                                                }).ToList().ForEach(r => Console.WriteLine(JsonConvert.SerializeObject(r)));                
            }
        }
    }
}
