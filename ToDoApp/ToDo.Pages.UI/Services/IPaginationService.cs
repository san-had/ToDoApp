using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Extensibility.Dto;

namespace ToDo.Pages.UI.Services
{
    public interface IPaginationService
    {
        Task<IEnumerable<ToDoDto>> GetTodos(FilterDto filter, int currentPage);

        Task<int> GetPageCount(FilterDto filter);
    }
}