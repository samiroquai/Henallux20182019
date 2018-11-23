using System;

namespace api.DTO
{
    public class StudentCourse
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public int? Cote { get; set; }

    }
}
