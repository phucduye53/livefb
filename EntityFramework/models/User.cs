using System.Collections.Generic;
using liveBot.module;
using livefb.Repository.Entity;

namespace liveBot.EntityFramework.models
{
    public class User : AuditableEntity<int>
    {
            public string FacebookId {get;set;}
            public string DisplayName { get; set; }

            public string NormalizedName { get; set; }
            public ICollection<Comment> Comments { get; set; }

            public User()
            {
                Comments = new List<Comment>();
            }
            public User(commentResult result)
            {
                FacebookId = result.data.from.id;
                DisplayName = result.data.from.name;
                NormalizedName = DisplayName.ToLower().Normalize();
                Comments = new List<Comment>();                            
            }

            public bool SetDisplayName(string DisplayName)
            {
                if(DisplayName != this.DisplayName)
                {
                    this.DisplayName = DisplayName;
                    return true;
                }
                return false;
            }

    }
}