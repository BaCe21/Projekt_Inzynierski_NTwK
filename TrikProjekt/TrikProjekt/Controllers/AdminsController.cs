using Microsoft.AspNetCore.Mvc;

namespace TrikProjekt56.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult Index()
        {
            List<string> admins = Admins.admins;
            return View(admins);
        }
    }
}
