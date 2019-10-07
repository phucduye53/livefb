using System.Threading.Tasks;
using liveBot.EntityFramework.models;
using liveBot.Repository;
using livefb.EntityFramework.models;

namespace livefb.Repository
{
    public interface IUnitOfWork
    {
         IRepository<User,int> UserRepository {get;}
         IRepository<Comment,long> CommentRepository {get;}
         IRepository<StreamSesson,int> StreamSessonReporitory {get;}
         int SaveChanges();
         Task<int> SaveChangesAsync();
    }
}