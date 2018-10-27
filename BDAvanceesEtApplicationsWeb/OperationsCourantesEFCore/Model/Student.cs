using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Student
    {
        public Student()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public long Id { get; set; }
        public DateTime? Birthdate { get; set; }
        public string FullName { get; set; }
        public string Remark { get; set; }

        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
