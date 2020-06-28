using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ToDoDto> GetAll(FilterDto filter, PagingDto paging)
        {
            IQueryable<ToDoDbModel> toDbModels = FilterToDoDbModel(filter);

            foreach (var toDoDbModel in toDbModels.Skip(paging.PageNumber * paging.PageSize).Take(paging.PageSize))
            {
                yield return toDoEntityConverter.Convert(toDoDbModel);
            }
        }

        public ToDoDto GetToDoItemById(int id)
        {
            var toDoDbModel = dbContext.ToDos.FirstOrDefault(t => t.Id == id);
            return toDoEntityConverter.Convert(toDoDbModel);
        }

        public void Update(ToDoDto toDoDto)
        {
            var toDoDbModel = dbContext.ToDos.FirstOrDefault(t => t.Id == toDoDto.Id);
            toDoDbModel.Description = toDoDto.Description;
            toDoDbModel.IsCompleted = toDoDto.IsCompleted;
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var toDoDbModel = dbContext.ToDos.FirstOrDefault(t => t.Id == id);
            dbContext.Remove(toDoDbModel);
            dbContext.SaveChanges();
        }

        public int GetAllRecordCount(FilterDto filter)
        {
            IQueryable<ToDoDbModel> toDbModels = FilterToDoDbModel(filter);

            return toDbModels.Count();
        }

        private IQueryable<ToDoDbModel> FilterToDoDbModel(FilterDto filter)
        {
            IQueryable<ToDoDbModel> toDbModels = dbContext.ToDos;

            if (!string.IsNullOrEmpty(filter.DescriptionFilter))
            {
                toDbModels = toDbModels.Where(t => t.Description.StartsWith(filter.DescriptionFilter));
            }

            if (filter.BothFilter.HasValue && !filter.BothFilter.Value)
            {
                toDbModels = toDbModels.Where(t => t.IsCompleted == filter.IsCompletedFilter);
            }

            return toDbModels;
        }
    }
}