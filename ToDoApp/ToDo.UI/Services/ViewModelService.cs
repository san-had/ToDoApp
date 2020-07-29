using Microsoft.Extensions.Options;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;
using ToDo.UI.Converters;
using ToDo.UI.Models;

namespace ToDo.UI.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly int pageSize;

        private readonly IToDoService toDoService;
        private readonly IToDoConverter toDoConverter;

        public ViewModelService(
            IOptionsSnapshot<ConfigurationSettings> configurationSettings,
            IToDoService toDoService,
            IToDoConverter toDoConverter)
        {
            pageSize = configurationSettings.Value.PageSize;
            this.toDoService = toDoService;
            this.toDoConverter = toDoConverter;
        }

        public ToDoItemViewModel GetItemById(int id)
        {
            var toDoDto = toDoService.GetToDoItemById(id);
            return toDoConverter.Convert(toDoDto);
        }

        public int AddItem(ToDoItemViewModel toDoItem)
        {
            var toDoDto = toDoConverter.Convert(toDoItem);
            return toDoService.CreateToDoItem(toDoDto);
        }

        public void UpdateItem(ToDoItemViewModel toDoItem)
        {
            var toDoDto = toDoConverter.Convert(toDoItem);
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
            var toDoItemViewList = toDoConverter.ConvertToViewModelList(toDos);

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
    }
}