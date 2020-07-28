using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;
using ToDo.UI.Models;

namespace ToDo.UI.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly int pageSize;

        private readonly IToDoService toDoService;

        public ViewModelService(
            IOptionsSnapshot<ConfigurationSettings> configurationSettings,
            IToDoService toDoService)
        {
            pageSize = configurationSettings.Value.PageSize;
            this.toDoService = toDoService;
        }

        public ToDoItemViewModel GetItemById(int id)
        {
            var toDoDto = toDoService.GetToDoItemById(id);
            return ConvertToViewModel(toDoDto);
        }

        public int AddItem(ToDoItemViewModel toDoItem)
        {
            var toDoDto = ConvertToToDoDto(toDoItem);
            return toDoService.CreateToDoItem(toDoDto);
        }

        public void UpdateItem(ToDoItemViewModel toDoItem)
        {
            var toDoDto = ConvertToToDoDto(toDoItem);
            toDoService.UpdateToDoItem(toDoDto);
        }

        public void DeleteItem(int id)
        {
            toDoService.DeleteToDoItem(id);
        }

        public ToDoItemListViewModel GetToDoList(FilterDto filter, int currentPage)
        {
            var paging = new PagingDto
            {
                PageSize = pageSize,
                PageNumber = currentPage
            };

            var toDos = toDoService.GetAll(filter, paging);
            var toDoItemViewList = ConvertToViewModelList(toDos);

            int recordCount = toDoService.GetAllRecordCount(filter);
            var viewModel = new ToDoItemListViewModel
            {
                ToDoItemViewList = toDoItemViewList,
                PageCount = toDoService.GetPageCount(recordCount, pageSize),
                DescriptionFilter = filter.DescriptionFilter,
                IsCompletedFilter = filter.IsCompletedFilter,
                BothFilter = filter.BothFilter,
                CurrentPage = currentPage
            };

            return viewModel;
        }

        private List<ToDoItemViewModel> ConvertToViewModelList(IEnumerable<ToDoDto> toDos)
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

        private ToDoItemViewModel ConvertToViewModel(ToDoDto toDo)
        {
            ToDoItemViewModel viewModel = new ToDoItemViewModel
            {
                Id = toDo.Id,
                Description = toDo.Description,
                IsCompleted = toDo.IsCompleted
            };
            return viewModel;
        }

        private ToDoDto ConvertToToDoDto(ToDoItemViewModel viewModel)
        {
            ToDoDto toDo = new ToDoDto
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                IsCompleted = viewModel.IsCompleted
            };
            return toDo;
        }
    }
}