using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Texxty.Models;

namespace Texxty.Repository.Classes
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TexxtyDBEntitiesMVC context;

        public UserRepository()
        {
            this.context = new TexxtyDBEntitiesMVC();
        }

        public bool CheckUsernameAvailable(User user) =>
            context.Users.Where(x => x.Username == user.Username).FirstOrDefault() == null;

        public bool CheckEmailAvailable(User user) =>
            context.Users.Where(x => x.Email == user.Email).FirstOrDefault() == null;
    }
}