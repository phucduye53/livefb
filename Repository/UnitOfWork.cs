
using System;
using System.Threading.Tasks;
using liveBot.EntityFramework;
using liveBot.EntityFramework.models;

namespace liveBot.Repository
{
    public class UnitOfWork : IDisposable
    {
        public FBDBContext DbContext { get; }

        public IRepository<User> UserRepository { get; }
        public IRepository<Comment> CommentRepository { get; }

        public UnitOfWork(FBDBContext context,
             IRepository<User> user,
             IRepository<Comment> comment)
        {
            DbContext = context;
            UserRepository = user;
            UserRepository.DbContext = DbContext;

            CommentRepository = comment;
            CommentRepository.DbContext = DbContext;    
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