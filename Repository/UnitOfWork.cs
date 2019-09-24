
using System;
using System.Threading.Tasks;
using liveBot.EntityFramework;
using liveBot.EntityFramework.models;
using livefb.Repository;

namespace liveBot.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FBDBContext DbContext;

        private IRepository<User> userRepository;
        private IRepository<Comment> commentRepository;

        public UnitOfWork(FBDBContext context)
        {
            DbContext = context;
        }
        public IRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User>(DbContext);
                }
                return userRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {

                if (this.commentRepository == null)
                {
                    this.commentRepository = new Repository<Comment>(DbContext);
                }
                return commentRepository;
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