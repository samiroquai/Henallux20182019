using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Course
    {
        public Course()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public long Id { get; set; }
        public string Description { get; set; }

        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
