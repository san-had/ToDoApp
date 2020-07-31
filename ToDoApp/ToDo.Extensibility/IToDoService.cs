using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoService
    {
        Task<int> CreateToDoItem(ToDoDto toDoDto);

        Task<IEnumerable<ToDoDto>> GetAll(FilterDto filter, PagingDto paging);

        Task<int> GetAllRecordCount(FilterDto filter);

        int GetPageCount(int recordCount, int pageSize);

        Task<ToDoDto> GetToDoItemById(int id);

        Task UpdateToDoItem(ToDoDto toDoDto);

        Task DeleteToDoItem(int id);
    }
}