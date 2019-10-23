using System;
using System.Linq;
using System.Threading.Tasks;
using liveBot.EntityFramework.models;
using livefb.Helper;
using livefb.Models;
using livefb.Services.Comments;
using livefb.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace livefb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommentService _ICommentService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(string currentFilter, string searchString,int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            var users = _userService.GetUsers(searchString);


            users = users.OrderBy(p=>p.DisplayName);


            int pageSize = 5;

            return View(await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> Details(int? id,int? pageNumber)
        {
                if (id == null)
                {
                  return NotFound();
                }





            var user = _userService.GetUsers(id).Select(p=>new UserViewModel{
                FacebookId = p.FacebookId,
                DisplayName = p.DisplayName
            }).FirstOrDefault() ;

            var commentsQuery = _ICommentService.GetCommentsByUserId(id).GroupBy(cP => new {cP.StreamSessonId}).Select(p=>new StreamSessonViewModel{
                    CreatedDate = p.FirstOrDefault().CreatedDate,
                    Comments = p.Select(pC=>new CommentViewModel{
                        DisplayName = pC.DisplayName,
                        CreatedDate = pC.CreatedDate,
                        Message = pC.Message
                }).ToList()
            });

            commentsQuery.OrderBy(p=>p.CreatedDate);
            

            int pageSize = 5;

            var comments = await PaginatedList<StreamSessonViewModel>.CreateAsync(commentsQuery.AsNoTracking(), pageNumber ?? 1, pageSize);



            var result = new UserViewDetailModel(user,comments);

            return View(result);
        }
    }
}