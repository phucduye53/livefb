using System.Collections.Generic;

namespace livefb.Models
{
    public class UserViewDetailModel
    {
        public UserViewModel User {get;set;}

        public ICollection<StreamSessonViewModel> Sessons {get;set;}

        public UserViewDetailModel(){}

        public UserViewDetailModel(UserViewModel user,List<StreamSessonViewModel> Sessons)
        {
            this.User= user;
            Sessons = new List<StreamSessonViewModel>();
            this.Sessons = Sessons;
        }
    }
}