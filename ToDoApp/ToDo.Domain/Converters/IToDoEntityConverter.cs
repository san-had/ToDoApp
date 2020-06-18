using ToDo.Domain.Database.Model;
using ToDo.Extensibility.Dto;

namespace ToDo.Domain.Converters
{
    public interface IToDoEntityConverter :
        IEntityConverter<ToDoDto, ToDoDbModel>,
        IEntityConverter<ToDoDbModel, ToDoDto>
    {
    }
}