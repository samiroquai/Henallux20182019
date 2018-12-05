using System;
namespace DDDDemo.Model
{
    public class BusinessException:Exception
    {
        public BusinessException(string message)
            :base(message)
        {
        }
    }
}
