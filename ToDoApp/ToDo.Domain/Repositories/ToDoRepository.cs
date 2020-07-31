using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> CreateAsync(ToDoDto toDoDto)
        {
            ToDoDbModel toDo = toDoEntityConverter.Convert(toDoDto);
            await dbContext.AddAsync(toDo);
            await dbContext.SaveChangesAsync();
            return toDo.Id;
        }

        public async Task<IEnumerable<ToDoDto>> GetAllAsync(FilterDto filter, PagingDto paging)
        {
            IQueryable<ToDoDbModel> toDbModels = FilterToDoDbModel(filter);

            var toDbModelsList = await toDbModels.Skip(paging.PageNumber * paging.PageSize).Take(paging.PageSize).ToListAsync();

            return toDbModelsList.Select(toDbModel => toDoEntityConverter.Convert(toDbModel));
        }

        public async Task<ToDoDto> GetToDoItemByIdAsync(int id)
        {
            var toDoDbModel = await dbContext.ToDos.FirstOrDefaultAsync(t => t.Id == id);
            return toDoEntityConverter.Convert(toDoDbModel);
        }

        public async Task UpdateAsync(ToDoDto toDoDto)
        {
            var toDoDbModel = dbContext.ToDos.FirstOrDefault(t => t.Id == toDoDto.Id);
            toDoDbModel.Description = toDoDto.Description;
            toDoDbModel.IsCompleted = toDoDto.IsCompleted;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var toDoDbModel = dbContext.ToDos.FirstOrDefault(t => t.Id == id);
            dbContext.Remove(toDoDbModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task<int> GetAllRecordCountAsync(FilterDto filter)
        {
            IQueryable<ToDoDbModel> toDbModels = FilterToDoDbModel(filter);

            return await toDbModels.CountAsync();
        }

        private IQueryable<ToDoDbModel> FilterToDoDbModel(FilterDto filter)
        {
            IQueryable<ToDoDbModel> toDbModels = dbContext.ToDos;

            if (filter == null)
            {
                return toDbModels;
            }

            if (!string.IsNullOrEmpty(filter?.DescriptionFilter))
            {
                toDbModels = toDbModels.Where(t => t.Description.StartsWith(filter.DescriptionFilter));
            }

            if (!filter.BothFilter.HasValue || filter.BothFilter.HasValue && !filter.BothFilter.Value)
            {
                toDbModels = toDbModels.Where(t => t.IsCompleted == filter.IsCompletedFilter);
            }

            return toDbModels;
        }
    }
}