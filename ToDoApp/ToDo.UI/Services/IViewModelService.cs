using ToDo.Extensibility.Dto;
using ToDo.UI.Models;

namespace ToDo.UI.Services
{
    public interface IViewModelService
    {
        ToDoItemListViewModel GetToDoList(FilterDto filter, int currentPage);

        int AddItem(ToDoItemViewModel toDoItem);

        ToDoItemViewModel GetItemById(int id);

        void UpdateItem(ToDoItemViewModel toDoItem);

        void DeleteItem(int id);
    }
}