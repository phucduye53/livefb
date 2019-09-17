using livefb.Repository.Entity;

namespace liveBot.EntityFramework.models
{
    public class Comment : AuditableEntity<long>
    {
        public string CommentId {get;set;}

        public string Message {get;set;}
        public int UserId { get; set; }
        public User User { get; set; }
    }
}