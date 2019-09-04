namespace liveBot.module
{
   public class LiveVideoResult
        {
               public StreamVideo[] data { get; set; }
               public pagingClass paging { get; set; }
        }

           public class StreamVideo
            {
           public string id { get; set; }
            public string stream_url { get; set; }
            public string secure_stream_url { get; set; }
         }
        
        public class commentResult{
            public commentClass data {get;set;}
        }
        public class commentClass {
            public string created_time { get; set; }
            public class fromClass {
                public string name { get; set; }
                public string id { get; set; }
            }
            public fromClass from { get; set; }
            public string message { get; set; }
            public string id { get; set; }
        }

        public class pagingClass {
            public class cursorClass {
                public string before { get; set; }
                public string after { get; set; }
            }
            public cursorClass cursor { get; set; }
            public string next { get; set; }
        }

        public class Comment {
            public commentClass[] data { get; set; }
            public pagingClass paging { get; set; }
        }
}