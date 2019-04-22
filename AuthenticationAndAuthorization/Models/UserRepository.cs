using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationAndAuthorization.Models
{
    public class UserRepository
    {
        public UserRepository()
        {
           // List<Role> lstRole = new List<Role>()
           // {
           //     new Role() {RoleId=1,RoleName="SuperAdmin" },
           //     new Role() {RoleId=2,RoleName="Admin" }
           // };

           //List<User> obj = new List<User>()
           // {
           //     new User() { UserId=1, Username="Hariom",Password="123", RoleId=1  },
           //     new User() { UserId=1, Username="Om",Password="1234", RoleId=2  }
           // };

        }
        public List<User> AllUser()
        {
            List<User> obj = new List<User>()
            {
                new User() { UserId=1, Username="Hariom",Password="123", RoleId=1  },
                new User() { UserId=1, Username="Om",Password="1234", RoleId=2  }
            };
            return obj;
        }
        public List<Role> AllRoles()
        {
            List<Role> lstRole = new List<Role>()
             {
                 new Role() {RoleId=1,RoleName="SuperAdmin" },
                 new Role() {RoleId=2,RoleName="Admin" }
             };

            return lstRole;
        }
    }
}