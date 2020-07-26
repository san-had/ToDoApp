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

        public ToDoDto GetItemById(int id)
        {
            return toDoService.GetToDoItemById(id);
        }

        public int AddItem(ToDoDto toDo)
        {
            return toDoService.CreateToDoItem(toDo);
        }

        public void UpdateItem(ToDoDto toDo)
        {
            toDoService.UpdateToDoItem(toDo);
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
            var toDoItemViewList = ConvertToViewModel(toDos);

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