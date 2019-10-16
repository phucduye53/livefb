using System;
using System.Linq;
using System.Threading.Tasks;
using liveBot.EntityFramework.models;
using livefb.Helper;
using livefb.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace livefb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
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
    }
}