using System.Threading.Tasks;
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

        public async Task<ToDoItemViewModel> GetItemByIdAsync(int id)
        {
            var toDoDto = await toDoService.GetToDoItemByIdAsync(id);
            return toDoConverter.Convert(toDoDto);
        }

        public async Task<int> AddItemAsync(ToDoItemViewModel toDoItem)
        {
            var toDoDto = toDoConverter.Convert(toDoItem);
            return await toDoService.CreateToDoItemAsync(toDoDto);
        }

        public async Task UpdateItemAsync(ToDoItemViewModel toDoItem)
        {
            var toDoDto = toDoConverter.Convert(toDoItem);
            await toDoService.UpdateToDoItemAsync(toDoDto);
        }

        public async Task DeleteItemAsync(int id)
        {
            await toDoService.DeleteToDoItemAsync(id);
        }

        public async Task<ToDoItemListViewModel> GetToDoListAsync(FilterDto filter, int currentPage)
        {
            var paging = new PagingDto
            {
                PageSize = pageSize,
                PageNumber = currentPage
            };

            var toDos = await toDoService.GetAllAsync(filter, paging);
            var toDoItemViewList = toDoConverter.ConvertToViewModelList(toDos);

            int recordCount = await toDoService.GetAllRecordCountAsync(filter);
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