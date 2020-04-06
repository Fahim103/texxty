using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texxty_api.Models;

namespace Texxty_api.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool CheckUsernameAvailable(User user);
        bool CheckEmailAvailable(User user);
    }
}
