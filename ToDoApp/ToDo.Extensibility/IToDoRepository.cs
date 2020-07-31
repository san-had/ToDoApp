using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoRepository
    {
        Task<int> Create(ToDoDto toDoDto);

        Task<IEnumerable<ToDoDto>> GetAll(FilterDto filter, PagingDto paging);

        Task<int> GetAllRecordCount(FilterDto filter);

        Task<ToDoDto> GetToDoItemById(int id);

        Task Update(ToDoDto toDoDto);

        Task Delete(int id);
    }
}