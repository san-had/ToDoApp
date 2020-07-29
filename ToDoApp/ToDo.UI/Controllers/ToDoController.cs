using System.Diagnostics;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index()
        {
            var filter = new FilterDto
            {
                BothFilter = true
            };
            var model = await viewModelService.GetToDoList(filter, StartPageIndex);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int currentPageIndex, ToDoItemListViewModel itemListViewModel)
        {
            var filter = new FilterDto()
            {
                DescriptionFilter = itemListViewModel.DescriptionFilter,
                IsCompletedFilter = itemListViewModel.IsCompletedFilter,
                BothFilter = itemListViewModel.BothFilter ?? true
            };
            var model = await viewModelService.GetToDoList(filter, currentPageIndex);
            return View(model);
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(FilterViewModel filterViewModel)
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
                    var model = await viewModelService.GetToDoList(filter, StartPageIndex);
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
        public async Task<IActionResult> Create(ToDoItemViewModel toDoItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await viewModelService.AddItem(toDoItem);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(toDoItem);
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occured saving task.");
            }
            return View(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var toDoItem = await viewModelService.GetItemById(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ToDoItemViewModel toDoItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await viewModelService.UpdateItem(toDoItem);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(toDoItem);
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occured saving task");
            }

            return View("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await viewModelService.DeleteItem(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}