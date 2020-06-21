using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDo.UI.Models;

namespace ToDo.UI.Controllers
{
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {
            //throw new InvalidOperationException("Something went wrong");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}