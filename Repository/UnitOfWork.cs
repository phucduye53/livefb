
using System;
using System.Threading.Tasks;
using liveBot.EntityFramework;
using liveBot.EntityFramework.models;
using livefb.EntityFramework.models;
using livefb.Repository;

namespace liveBot.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FBDBContext DbContext;

        private IRepository<User,int> userRepository;
        private IRepository<Comment,long> commentRepository;

        private IRepository<StreamSesson,int> streamRepository;

        public UnitOfWork(FBDBContext context)
        {
            DbContext = context;
        }
        public IRepository<User,int> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User,int>(DbContext);
                }
                return userRepository;
            }
        }

        public IRepository<Comment,long> CommentRepository
        {
            get
            {

                if (this.commentRepository == null)
                {
                    this.commentRepository = new Repository<Comment,long>(DbContext);
                }
                return commentRepository;
            }
        }
        public IRepository<StreamSesson,int> StreamSessonReporitory
        {
            get
            {

                if (this.streamRepository == null)
                {
                    this.streamRepository = new Repository<StreamSesson,int>(DbContext);
                }
                return streamRepository;
            }
        }
        
        
        public int SaveChanges()
        {
            var iResult = DbContext.SaveChanges();
            return iResult;
        }

        public async Task<int> SaveChangesAsync()
        {
            var iResult = await DbContext.SaveChangesAsync();
            return iResult;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}