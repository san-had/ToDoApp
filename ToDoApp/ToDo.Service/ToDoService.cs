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

        public async Task<int> CreateToDoItemAsync(ToDoDto toDoDto)
        {
            return await toDoRepository.CreateAsync(toDoDto);
        }

        public async Task<IEnumerable<ToDoDto>> GetAllAsync(FilterDto filter, PagingDto paging)
        {
            return await toDoRepository.GetAllAsync(filter, paging);
        }

        public async Task<ToDoDto> GetToDoItemByIdAsync(int id)
        {
            return await toDoRepository.GetToDoItemByIdAsync(id);
        }

        public async Task UpdateToDoItemAsync(ToDoDto toDoDto)
        {
            await toDoRepository.UpdateAsync(toDoDto);
        }

        public async Task DeleteToDoItemAsync(int id)
        {
            await toDoRepository.DeleteAsync(id);
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

        public async Task<int> GetAllRecordCountAsync(FilterDto filter)
        {
            return await toDoRepository.GetAllRecordCountAsync(filter);
        }
    }
}