using System.Collections.Generic;
using livefb.Repository.Entity;

namespace liveBot.EntityFramework.models
{
    public class User : AuditableEntity<int>
    {
            public string FacebookId {get;set;}
            public string DisplayName { get; set; }

            public ICollection<Comment> Comments { get; set; }
    }
}