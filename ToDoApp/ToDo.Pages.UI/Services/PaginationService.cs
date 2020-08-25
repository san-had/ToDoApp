using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;

namespace ToDo.Pages.UI.Services
{
    public class PaginationService : IPaginationService
    {
        private readonly IToDoService toDoService;

        public PaginationService(
            IOptionsSnapshot<ConfigurationSettings> configurationSettings,
            IToDoService toDoService)
        {
            PageSize = configurationSettings.Value.PageSize;
            this.toDoService = toDoService;
        }

        public int PageSize { get; set; }

        public async Task<IList<ToDoDto>> GetTodos(FilterDto filter, int currentPage)
        {
            var paging = new PagingDto
            {
                PageNumber = currentPage,
                PageSize = PageSize
            };

            return (await toDoService.GetAllAsync(filter, paging)).ToList();
        }

        public async Task<int> GetPageCount(FilterDto filter)
        {
            int recordCount = await toDoService.GetAllRecordCountAsync(filter);
            return toDoService.GetPageCount(recordCount, PageSize);
        }
    }
}