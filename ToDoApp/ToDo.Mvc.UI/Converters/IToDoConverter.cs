using System.Collections.Generic;
using ToDo.Extensibility.Dto;
using ToDo.Mvc.UI.Models;

namespace ToDo.Mvc.UI.Converters
{
    public interface IToDoConverter :
        IConverter<ToDoDto, ToDoItemViewModel>,
        IConverter<ToDoItemViewModel, ToDoDto>
    {
        List<ToDoItemViewModel> ConvertToViewModelList(IEnumerable<ToDoDto> toDos);
    }
}