using System.Collections.Generic;
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

        public IEnumerable<ToDoDto> GetAll()
        {
            return toDoRepository.GetAll();
        }

        public ToDoDto GetToDoItemById(int id)
        {
            return toDoRepository.GetToDoItemById(id);
        }

        public void UpdateToDoItem(ToDoDto toDoDto)
        {
            toDoRepository.Update(toDoDto);
        }

        public void DeleteToDoItem(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}