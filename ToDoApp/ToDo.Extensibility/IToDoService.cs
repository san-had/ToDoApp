using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoService
    {
        int CreateToDoItem(ToDoDto toDoDto);
    }
}