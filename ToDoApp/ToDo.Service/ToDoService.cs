using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<int> CreateToDoItem(ToDoDto toDoDto)
        {
            return await toDoRepository.Create(toDoDto);
        }

        public async Task<IEnumerable<ToDoDto>> GetAll(FilterDto filter, PagingDto paging)
        {
            return await toDoRepository.GetAll(filter, paging);
        }

        public async Task<ToDoDto> GetToDoItemById(int id)
        {
            return await toDoRepository.GetToDoItemById(id);
        }

        public async Task UpdateToDoItem(ToDoDto toDoDto)
        {
            await toDoRepository.Update(toDoDto);
        }

        public async Task DeleteToDoItem(int id)
        {
            await toDoRepository.Delete(id);
        }

        public int GetPageCount(int recordCount, int pageSize)
        {
            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize != 0)
            {
                pageCount += 1;
            }

            return pageCount;
        }

        public async Task<int> GetAllRecordCount(FilterDto filter)
        {
            return await toDoRepository.GetAllRecordCount(filter);
        }
    }
}