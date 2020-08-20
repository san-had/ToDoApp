using System.Collections.Generic;
using ToDo.Extensibility.Dto;
using ToDo.Mvc.UI.Models;

namespace ToDo.Mvc.UI.Converters
{
    public class ToDoConverter : IToDoConverter
    {
        public ToDoItemViewModel Convert(ToDoDto source)
        {
            ToDoItemViewModel viewModel = new ToDoItemViewModel
            {
                Id = source.Id,
                Description = source.Description,
                IsCompleted = source.IsCompleted
            };
            return viewModel;
        }

        public ToDoDto Convert(ToDoItemViewModel source)
        {
            ToDoDto toDo = new ToDoDto
            {
                Id = source.Id,
                Description = source.Description,
                IsCompleted = source.IsCompleted
            };
            return toDo;
        }

        public List<ToDoItemViewModel> ConvertToViewModelList(IEnumerable<ToDoDto> toDos)
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