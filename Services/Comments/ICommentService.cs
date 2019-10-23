using System.Linq;
using liveBot.EntityFramework.models;

namespace livefb.Services.Comments
{
    public interface ICommentService
    {
        IQueryable<Comment> GetCommentsByUserId(int? userId);
    }
}