using System;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    public class StudentNotFoundException : Exception
    {
        private long studentId;

        public StudentNotFoundException()
        {
        }

        public StudentNotFoundException(long studentId)
        {
            this.studentId = studentId;
        }

        public StudentNotFoundException(string message) : base(message)
        {
        }

        public StudentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StudentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}