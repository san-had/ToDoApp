using ToDo.Extensibility;
using ToDo.Extensibility.Dto;

namespace ToDo.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }

        public int CreateToDoItem(ToDoDto toDoDto)
        {
            return toDoRepository.Create(toDoDto);
        }
    }
}