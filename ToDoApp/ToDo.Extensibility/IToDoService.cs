using System.Collections.Generic;
using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoService
    {
        int CreateToDoItem(ToDoDto toDoDto);

        IEnumerable<ToDoDto> GetAll(FilterDto filter);

        ToDoDto GetToDoItemById(int id);

        void UpdateToDoItem(ToDoDto toDoDto);

        void DeleteToDoItem(int id);
    }
}