using System.Threading.Tasks;
using liveBot.EntityFramework.models;
using liveBot.Repository;
using livefb.EntityFramework.models;

namespace livefb.Repository
{
    public interface IUnitOfWork
    {
         IRepository<User> UserRepository {get;}
         IRepository<Comment> CommentRepository {get;}
         IRepository<StreamSesson> StreamSessonReporitory {get;}
         int SaveChanges();
         Task<int> SaveChangesAsync();
    }
}