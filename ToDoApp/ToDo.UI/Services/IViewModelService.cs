using System.Threading.Tasks;
using ToDo.Extensibility.Dto;
using ToDo.UI.Models;

namespace ToDo.UI.Services
{
    public interface IViewModelService
    {
        Task<ToDoItemListViewModel> GetToDoList(FilterDto filter, int currentPage);

        Task<int> AddItem(ToDoItemViewModel toDoItem);

        Task<ToDoItemViewModel> GetItemById(int id);

        Task UpdateItem(ToDoItemViewModel toDoItem);

        Task DeleteItem(int id);
    }
}