// UserRepository.cs
using System.Collections.Generic;

namespace ProductManagementApp
{
    public class UserRepository
    {
        private List<User> users = new List<User>
        {
            new User("thanh", "2309", UserRole.Admin),
            new User("user", "user123", UserRole.User)
        };

        public User Authenticate(string username, string password)
        {
            return users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
