using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;

namespace ToDo.Pages.UI.Services
{
    public class PaginationService : IPaginationService
    {
        private readonly int pageSize;
        private readonly IToDoService toDoService;

        public PaginationService(
            IOptionsSnapshot<ConfigurationSettings> configurationSettings,
            IToDoService toDoService)
        {
            pageSize = configurationSettings.Value.PageSize;
            this.toDoService = toDoService;
        }

        public async Task<IEnumerable<ToDoDto>> GetTodos(FilterDto filter, int currentPage)
        {
            var paging = new PagingDto
            {
                PageNumber = currentPage,
                PageSize = pageSize
            };

            return await toDoService.GetAllAsync(filter, paging);
        }

        public async Task<int> GetPageCount(FilterDto filter)
        {
            int recordCount = await toDoService.GetAllRecordCountAsync(filter);
            return toDoService.GetPageCount(recordCount, pageSize);
        }
    }
}