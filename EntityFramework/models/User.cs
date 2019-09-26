using System.Collections.Generic;
using liveBot.module;
using livefb.Repository.Entity;

namespace liveBot.EntityFramework.models
{
    public class User : AuditableEntity<int>
    {
            public string FacebookId {get;set;}
            public string DisplayName { get; set; }

            public ICollection<Comment> Comments { get; set; }

            public User()
            {
                Comments = new List<Comment>();
            }
            public User(commentResult result)
            {
                FacebookId = result.data.from.id;
                DisplayName = result.data.from.name;
                Comments = new List<Comment>();
                
                
            }

    }
}