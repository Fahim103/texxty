using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.CustomModels;

namespace TexxtyDataAccess.Repository.Classes
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TexxtyDBEntities context;

        public UserRepository()
        {
            this.context = new TexxtyDBEntities();
        }

        public UserModel GetUserModel(int id)
        {
            var entity = Get(id);
            return new UserModel
            {
                UserID = entity.UserID,
                Username = entity.Username,
                Email = entity.Email,
                FullName = entity.FullName,
                ActiveStatus = entity.ActiveStatus,
                Role = entity.Role,
            };
        }

        public bool CheckUsernameAvailable(User user) =>
            context.Users.Where(x => x.Username == user.Username).FirstOrDefault() == null;

        public bool CheckEmailAvailable(User user) =>
            context.Users.Where(x => x.Email == user.Email).FirstOrDefault() == null;
    }
}