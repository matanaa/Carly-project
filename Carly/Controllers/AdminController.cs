using Carly.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carly.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    
    // GET: Admin
    public ActionResult AdminList()
        {
            
           ApplicationDbContext db = new ApplicationDbContext();

            var usersAndRoles = (from u in db.Users
                                 from userRole in u.Roles
                                 join r in db.Roles on userRole.RoleId equals r.Id
                                 select new { u.UserName, u.Email, r.Name });

            var users = new List<Admin>();
            foreach (var u in usersAndRoles)
                users.Add(new Admin
                {
                    userName = u.UserName,
                    email = u.Email,
                    roleName = u.Name
                });
            ViewBag.UserList = users;
            return View();
        }
    }
}