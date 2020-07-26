using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDo.Extensibility.Dto;
using ToDo.UI.Models;
using ToDo.UI.Services;

namespace ToDo.UI.Controllers
{
    public class ToDoController : Controller
    {
        private const int StartPageIndex = 0;

        private readonly IViewModelService viewModelService;

        public ToDoController(IViewModelService viewModelService)
        {
            this.viewModelService = viewModelService;
        }

        public IActionResult Index()
        {
            var filter = new FilterDto();
            return View(viewModelService.GetToDoList(filter, StartPageIndex));
        }

        [HttpPost]
        public IActionResult Index(int currentPageIndex, ToDoItemListViewModel itemListViewModel)
        {
            var filter = new FilterDto()
            {
                DescriptionFilter = itemListViewModel.DescriptionFilter,
                IsCompletedFilter = itemListViewModel.IsCompletedFilter,
                BothFilter = itemListViewModel.BothFilter
            };
            var model = viewModelService.GetToDoList(filter, currentPageIndex);
            return View(model);
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(FilterViewModel filterViewModel)
        {
            var filter = new FilterDto
            {
                DescriptionFilter = filterViewModel.DescriptionFilter,
                IsCompletedFilter = filterViewModel.IsCompletedFilter,
                BothFilter = filterViewModel.BothFilter
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var model = viewModelService.GetToDoList(filter, StartPageIndex);
                    return View(nameof(Index), model);
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occured parsing filter object.");
            }
            return View(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToDoDto toDoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = viewModelService.AddItem(toDoDto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occured saving task.");
            }
            return View(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var toDo = viewModelService.GetItemById(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);
        }

        [HttpPost]
        public IActionResult Edit(ToDoDto toDo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModelService.UpdateItem(toDo);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occured saving task");
            }

            return View("Index");
        }

        public IActionResult Delete(int id)
        {
            viewModelService.DeleteItem(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}