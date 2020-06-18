using ToDo.Extensibility.Dto;

namespace ToDo.Extensibility
{
    public interface IToDoRepository
    {
        int Create(ToDoDto toDoDto);
    }
}