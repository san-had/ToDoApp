using System.Threading.Tasks;
using ToDo.Extensibility.Dto;
using ToDo.UI.Models;

namespace ToDo.UI.Services
{
    public interface IViewModelService
    {
        Task<ToDoItemListViewModel> GetToDoListAsync(FilterDto filter, int currentPage);

        Task<int> AddItemAsync(ToDoItemViewModel toDoItem);

        Task<ToDoItemViewModel> GetItemByIdAsync(int id);

        Task UpdateItemAsync(ToDoItemViewModel toDoItem);

        Task DeleteItemAsync(int id);
    }
}