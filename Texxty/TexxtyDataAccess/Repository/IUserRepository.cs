using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexxtyDataAccess.Models;

namespace TexxtyDataAccess.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool CheckUsernameAvailable(User user);
        bool CheckEmailAvailable(User user);
    }
}
