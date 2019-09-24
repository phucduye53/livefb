using liveBot.EntityFramework.models;
using livefb.Repository;

namespace livefb.Services.Users
{
    public class UserService: IUserService
    {
       private readonly IUnitOfWork unitOfWork;
       public UserService(IUnitOfWork unitOfWork)
       {
           this.unitOfWork = unitOfWork;
       }

        public bool CheckOut(User user)
        {
            if(!IsExist(user)) // LEGIT CHECK
            {
                unitOfWork.UserRepository.Add(user);
                unitOfWork.SaveChanges();
                return true;
            }else{
                //PROCESS
                return false;
            }
            
        }
        
        private bool IsExist(User user)
        {
            if(unitOfWork.UserRepository.GetById(user.Id)==null )
            {
                return false;
            }else{
                return true;
            }

        }
    }
}