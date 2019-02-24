using System;
namespace DDDDemo.Model
{
    public class User
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
