using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoRepository
    {
        Task<int> CreateAsync(ToDoDto toDoDto);

        Task<IEnumerable<ToDoDto>> GetAllAsync(FilterDto filter, PagingDto paging);

        Task<int> GetAllRecordCountAsync(FilterDto filter);

        Task<ToDoDto> GetToDoItemByIdAsync(int id);

        Task UpdateAsync(ToDoDto toDoDto);

        Task DeleteAsync(int id);
    }
}