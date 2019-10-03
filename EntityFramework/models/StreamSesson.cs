using System.Collections.Generic;
using liveBot.EntityFramework.models;
using livefb.Repository.Entity;

namespace livefb.EntityFramework.models
{
    public class StreamSesson : AuditableEntity<int>
    {
        public string StreamId {get;set;}
        public string StreamUrl {get;set;}
        public string StreamTitle {get;set;}
        public string SecureStreamUrl {get;set;}
        public ICollection<Comment> Comments { get; set; }
        public StreamSesson()
            {
                Comments = new List<Comment>();
            }
            public StreamSesson(liveBot.module.StreamVideo result)
            {
                StreamId = result.id;
                StreamUrl = result.stream_url;
                 SecureStreamUrl = result.secure_stream_url;
                Comments = new List<Comment>();
                
                
            }
        
    }
}