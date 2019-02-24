using System;
using System.Collections.Generic;
using DDDDemo.Model;

namespace DDDDemo.api.Infrastructure
{
    public class AuthenticationRepository
    {
        public AuthenticationRepository()
        {
        }

        private User[] _users = new User[]{
            new User(){ UserName="janedoe", Email="jane@doe.com", Id=1, Password="123"},
            new User(){ UserName="johndoe", Email="john@doe.com", Id=2, Password="456", Roles=new string[]{Constants.Roles.Admin, Constants.Roles.Gestionnaire} }

        };
        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }
}
