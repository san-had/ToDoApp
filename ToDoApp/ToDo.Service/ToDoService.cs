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

        public IEnumerable<ToDoDto> GetAll(FilterDto filter, PagingDto paging)
        {
            return toDoRepository.GetAll(filter, paging);
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
            toDoRepository.Delete(id);
        }

        public int GetAllRecordCount(FilterDto filter)
        {
            return toDoRepository.GetAllRecordCount(filter);
        }
    }
}