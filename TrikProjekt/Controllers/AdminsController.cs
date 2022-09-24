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
        public IActionResult AddAdmin(string name)
        {
            Admins.admins.Add(name);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddAdmin2()
        {
            Admins.admins.Add("test");        
            return RedirectToAction(nameof(Index));
        }
    }
}
