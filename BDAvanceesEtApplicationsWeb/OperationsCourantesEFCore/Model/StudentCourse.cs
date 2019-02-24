using System;
using System.Collections.Generic;

namespace Model
{
    public partial class StudentCourse
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public int? Cote { get; set; }
        public byte[] RowVersion { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
