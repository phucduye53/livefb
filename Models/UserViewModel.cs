using System.Collections.Generic;

namespace livefb.Models
{
    public class UserViewModel : AuditViewModel
    {
            public string FacebookId {get;set;}
            public string DisplayName { get; set; }

    }
}