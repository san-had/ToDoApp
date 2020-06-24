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
        private readonly IToDoService toDoService;
        private readonly PagingDto paging;

        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
            paging = new PagingDto
            {
                PageSize = 25,
                PageNumber = 0
            };
        }

        public IActionResult Index()
        {
            var filter = new FilterDto();
            var toDos = toDoService.GetAll(filter, paging);
            return View(toDos);
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(FilterDto filter)
        {
            IEnumerable<ToDoDto> toDos;
            try
            {
                if (ModelState.IsValid)
                {
                    toDos = toDoService.GetAll(filter, paging);
                    //return RedirectToAction(nameof(Index));
                    return View(nameof(Index), toDos);
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