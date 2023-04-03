using AuthProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProjectAPI.Tests.MockData
{
    public class UserMockData
    {

        public static User AuthUser(string email, string password)
        {
           var user = MockUserList().FirstOrDefault(_user => _user.Email == email && _user.Password == password);
           if(user == null) { return new User(); }
           return user;
        }

        private static List<User> MockUserList()
        {
            return new List<User> {
              new User
              {
                Id = 1,
                FirstName = "Ashutosh",
                LastName = "Tayade",
                Email = "ashutoshtayade3@gmail.com",
                Role = "User",
                Password= "Ashutosh@123"
              },
              new User
              {
                Id = 1,
                FirstName = "Parag",
                LastName = "Vatpal",
                Email = "paragvatpal@gmail.com",
                Role = "User",
                Password= "Parag@123"
              },
              new User
              {
                Id = 1,
                FirstName = "Sagar",
                LastName = "Bhopale",
                Email = "sagarbhopale@gmail.com",
                Role = "User",
                Password= "Sagar@123"
              },
           };
        }

    }
}
