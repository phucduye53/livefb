using System.Linq;
using liveBot.EntityFramework.models;
using livefb.Repository;

namespace livefb.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Comment> GetCommentsByUserId(int? userId)
        {
            return unitOfWork.CommentRepository.GetAll().Where(p=>p.UserId == userId);
        }
    }
}