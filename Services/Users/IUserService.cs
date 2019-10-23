using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liveBot.EntityFramework.models;

namespace livefb.Services.Users
{
    public interface IUserService
    {
        bool CheckOut(User user);
        IQueryable<User> GetUsers(string searchString);

        IQueryable<User> GetUsers(int? id);
    }
}