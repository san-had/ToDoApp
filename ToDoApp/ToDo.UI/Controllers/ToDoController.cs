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

        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        public IActionResult Index()
        {
            var filter = new FilterDto
            {
                //DescriptionFilter = "First",
                //IsCompletedFilter = true
            };

            var paging = new PagingDto
            {
                PageSize = 5,
                PageNumber = 1
            };

            var toDos = toDoService.GetAll(filter, paging);
            return View(toDos);
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