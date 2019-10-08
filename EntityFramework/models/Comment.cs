using liveBot.module;
using livefb.EntityFramework.models;
using livefb.Repository.Entity;

namespace liveBot.EntityFramework.models
{
    public class Comment : AuditableEntity<long>
    {
        public string CommentId {get;set;}
        public string Message {get;set;}
        public int UserId { get; set; }
        public User User { get; set; }

        public int StreamSessonId {get;set;}
        public StreamSesson StreamSesson {get;set;}

        public Comment()
        {}
        public Comment(commentClass result,User user,StreamSesson sesson)
        {
            CommentId = result.id;
            Message = result.message;
            StreamSessonId = sesson.Id;
            UserId = user.Id;


        }
    }
}