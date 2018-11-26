using System;
using System.Collections.Generic;

namespace api.DTO
{
    public class Student
    {
        public long Id { get; set; }
        public DateTime? Birthdate { get; set; }
        public string FullName { get; set; }
        public string Remark { get; set; }
        public byte[] RowVersion { get; set; }

        public IEnumerable<StudentCourse> StudentCourse { get; set; }
    }
}
