using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoService
    {
        Task<int> CreateToDoItemAsync(ToDoDto toDoDto);

        Task<IEnumerable<ToDoDto>> GetAllAsync(FilterDto filter, PagingDto paging);

        Task<int> GetAllRecordCountAsync(FilterDto filter);

        int GetPageCount(int recordCount, int pageSize);

        Task<ToDoDto> GetToDoItemByIdAsync(int id);

        Task UpdateToDoItemAsync(ToDoDto toDoDto);

        Task DeleteToDoItemAsync(int id);
    }
}