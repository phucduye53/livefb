using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liveBot.EntityFramework.models;
using livefb.Repository;
using Microsoft.EntityFrameworkCore;

namespace livefb.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool CheckOut(User user)
        {
            if (!IsExist(user)) // LEGIT CHECK
            {
                unitOfWork.UserRepository.Add(user);
                unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                //PROCESS
                return false;
            }

        }

        public IQueryable<User> GetUsers(string searchString)
        {
            var result = unitOfWork.UserRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(p => p.NormalizedName.Contains(searchString.ToLower().Normalize())
                    || p.FacebookId == searchString);
            }



            return result;
        }
        public IQueryable<User> GetUsers(int? id)
        {
            var result = unitOfWork.UserRepository.GetAll().Where(p=>p.Id==id);



            return result;
        }

        private bool IsExist(User user)
        {
            if (unitOfWork.UserRepository.GetById(user.Id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}