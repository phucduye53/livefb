using System.Threading.Tasks;
using liveBot.EntityFramework.models;
using liveBot.Repository;

namespace livefb.Repository
{
    public interface IUnitOfWork
    {
         IRepository<User> UserRepository {get;}
         IRepository<Comment> CommentRepository {get;}
         int SaveChanges();
         Task<int> SaveChangesAsync();
    }
}