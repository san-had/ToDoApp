using System.Collections.Generic;
using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoRepository
    {
        int Create(ToDoDto toDoDto);

        IEnumerable<ToDoDto> GetAll(FilterDto filter);

        ToDoDto GetToDoItemById(int id);

        void Update(ToDoDto toDoDto);

        void Delete(int id);
    }
}