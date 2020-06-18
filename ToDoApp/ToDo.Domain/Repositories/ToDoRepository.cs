using ToDo.Domain.Converters;
using ToDo.Domain.Database.Model;
using ToDo.Domain.Database.Providers;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;

namespace ToDo.Domain.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly MsSqlLiteDatabaseContext dbContext;
        private readonly IToDoEntityConverter toDoEntityConverter;

        public ToDoRepository(MsSqlLiteDatabaseContext dbContext, IToDoEntityConverter toDoEntityConverter)
        {
            this.dbContext = dbContext;
            this.toDoEntityConverter = toDoEntityConverter;
        }

        public int Create(ToDoDto toDoDto)
        {
            ToDoDbModel toDo = toDoEntityConverter.Convert(toDoDto);
            dbContext.Add(toDo);
            dbContext.SaveChanges();
            return toDo.Id;
        }
    }
}