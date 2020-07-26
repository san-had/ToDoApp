using ToDo.Extensibility.Dto;
using ToDo.UI.Models;

namespace ToDo.UI.Services
{
    public interface IViewModelService
    {
        ToDoItemListViewModel GetToDoList(FilterDto filter, int currentPage);

        int AddItem(ToDoDto toDo);

        ToDoDto GetItemById(int id);

        void UpdateItem(ToDoDto toDo);

        void DeleteItem(int id);
    }
}