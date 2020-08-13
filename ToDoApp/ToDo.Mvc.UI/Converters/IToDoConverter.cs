using System.Collections.Generic;
using ToDo.Extensibility.Dto;
using ToDo.UI.Models;

namespace ToDo.UI.Converters
{
    public interface IToDoConverter :
        IConverter<ToDoDto, ToDoItemViewModel>,
        IConverter<ToDoItemViewModel, ToDoDto>
    {
        List<ToDoItemViewModel> ConvertToViewModelList(IEnumerable<ToDoDto> toDos);
    }
}