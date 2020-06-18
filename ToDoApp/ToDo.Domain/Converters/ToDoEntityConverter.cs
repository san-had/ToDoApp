using ToDo.Domain.Database.Model;
using ToDo.Extensibility.Dto;

namespace ToDo.Domain.Converters
{
    public class ToDoEntityConverter : IToDoEntityConverter
    {
        public ToDoDbModel Convert(ToDoDto source)
        {
            return source != null
                ? new ToDoDbModel
                {
                    Id = source.Id,
                    Description = source.Description,
                    IsCompleted = source.IsCompleted
                } : null;
        }

        public ToDoDto Convert(ToDoDbModel source)
        {
            return source != null
                ? new ToDoDto
                {
                    Id = source.Id,
                    Description = source.Description,
                    IsCompleted = source.IsCompleted
                } : null;
        }
    }
}