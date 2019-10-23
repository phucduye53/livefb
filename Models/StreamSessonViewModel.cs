using System.Collections.Generic;

namespace livefb.Models
{
    public class StreamSessonViewModel : AuditViewModel
    {
        public ICollection<CommentViewModel> Comments {get;set;}

        public StreamSessonViewModel()
        {
            Comments = new List<CommentViewModel>();
        }
    }
}