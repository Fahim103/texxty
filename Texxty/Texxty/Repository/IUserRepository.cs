using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texxty.Models;

namespace Texxty.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool CheckUsernameAvailable(User user);
        bool CheckEmailAvailable(User user);
    }
}
