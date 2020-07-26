using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;
using ToDo.UI.Models;

namespace ToDo.UI.Controllers
{
    public class ToDoController : Controller
    {
        private const int PageSize = 25;
        private const int StartPageIndex = 0;

        private readonly IToDoService toDoService;

        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        public IActionResult Index()
        {
            var filter = new FilterDto();
            return View(GetToDoList(filter, StartPageIndex));
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
            var model = GetToDoList(filter, currentPageIndex);
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
                    var model = GetToDoList(filter, StartPageIndex);
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
                    int id = toDoService.CreateToDoItem(toDoDto);
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
            var toDo = toDoService.GetToDoItemById(id);
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
                    toDoService.UpdateToDoItem(toDo);
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
            toDoService.DeleteToDoItem(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private ToDoItemListViewModel GetToDoList(FilterDto filter, int currentPage)
        {
            var paging = new PagingDto
            {
                PageSize = PageSize,
                PageNumber = currentPage
            };

            var toDos = toDoService.GetAll(filter, paging);
            var toDoItemViewList = ConvertToViewModel(toDos);

            var viewModel = new ToDoItemListViewModel();
            viewModel.ToDoItemViewList = toDoItemViewList;
            viewModel.PageCount = toDoService.GetPageCount(filter, PageSize);
            viewModel.DescriptionFilter = filter.DescriptionFilter;
            viewModel.IsCompletedFilter = filter.IsCompletedFilter;
            viewModel.BothFilter = filter.BothFilter;
            viewModel.CurrentPage = currentPage;

            return viewModel;
        }

        private List<ToDoItemViewModel> ConvertToViewModel(IEnumerable<ToDoDto> toDos)
        {
            var toDoViewModelList = new List<ToDoItemViewModel>();
            foreach (var toDo in toDos)
            {
                var toDoItemViewModel = new ToDoItemViewModel()
                {
                    Id = toDo.Id,
                    Description = toDo.Description,
                    IsCompleted = toDo.IsCompleted
                };
                toDoViewModelList.Add(toDoItemViewModel);
            }
            return toDoViewModelList;
        }
    }
}