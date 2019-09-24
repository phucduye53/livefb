using liveBot.EntityFramework.models;

namespace livefb.Services.Users
{
    public interface IUserService
    {
        bool CheckOut(User user);
    }
}