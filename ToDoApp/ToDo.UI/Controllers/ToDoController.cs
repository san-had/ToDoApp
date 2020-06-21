using Microsoft.AspNetCore.Mvc;

namespace ToDo.UI.Controllers
{
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {
            //throw new InvalidOperationException("Something went wrong");
            return View();
        }
    }
}