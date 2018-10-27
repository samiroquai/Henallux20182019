using System;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    public class CourseNotFoundException : Exception
    {
        private long courseId;

        public CourseNotFoundException()
        {
        }

        public CourseNotFoundException(long courseId)
        {
            this.courseId = courseId;
        }

        public CourseNotFoundException(string message) : base(message)
        {
        }

        public CourseNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CourseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}