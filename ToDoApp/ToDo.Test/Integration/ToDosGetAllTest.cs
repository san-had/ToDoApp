using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ninject;
using NUnit.Framework;
using ToDo.Domain.Converters;
using ToDo.Domain.Database.Model;
using ToDo.Domain.Database.Providers;
using ToDo.Domain.Repositories;
using ToDo.Extensibility.Dto;
using ToDo.Service;

namespace ToDo.Test.Integration
{
    public class ToDosGetAllTest : TestBase
    {
        private static readonly List<ToDoDbModel> toDosForContextLoading = new List<ToDoDbModel>()
        {
            new ToDoDbModel { Id = 1, Description = "First Task", IsCompleted = false },
            new ToDoDbModel { Id = 2, Description = "Second Task", IsCompleted = false },
            new ToDoDbModel { Id = 3, Description = "Third Task", IsCompleted = false }
        };

        private static readonly List<ToDoDbModel> toDosForPaging = new List<ToDoDbModel>()
        {
            new ToDoDbModel { Id = 1, Description = "First Task", IsCompleted = false },
            new ToDoDbModel { Id = 2, Description = "Second Task", IsCompleted = false },
            new ToDoDbModel { Id = 3, Description = "Third Task", IsCompleted = false },
            new ToDoDbModel { Id = 4, Description = "Fourth Task", IsCompleted = false },
            new ToDoDbModel { Id = 5, Description = "Fifth Task", IsCompleted = false },
            new ToDoDbModel { Id = 6, Description = "Sixth Task", IsCompleted = false },
            new ToDoDbModel { Id = 7, Description = "Seventh Task", IsCompleted = false },
            new ToDoDbModel { Id = 8, Description = "Eighth Task", IsCompleted = false },
            new ToDoDbModel { Id = 9, Description = "Nineth Task", IsCompleted = false },
            new ToDoDbModel { Id = 10, Description = "Tenth Task", IsCompleted = false },
            new ToDoDbModel { Id = 11, Description = "Eleventh Task", IsCompleted = false },
            new ToDoDbModel { Id = 12, Description = "Twelveth Task", IsCompleted = false }
        };

        private static readonly List<ToDoDbModel> toDosForFiltering = new List<ToDoDbModel>()
        {
            new ToDoDbModel { Id = 1, Description = "First Task", IsCompleted = false },
            new ToDoDbModel { Id = 2, Description = "Second Task", IsCompleted = false },
            new ToDoDbModel { Id = 3, Description = "Third Task", IsCompleted = false },
            new ToDoDbModel { Id = 4, Description = "Fourth Task", IsCompleted = true },
            new ToDoDbModel { Id = 5, Description = "Fifth Task", IsCompleted = false },
            new ToDoDbModel { Id = 6, Description = "Sixth Task", IsCompleted = false },
            new ToDoDbModel { Id = 7, Description = "Seventh Task", IsCompleted = false },
            new ToDoDbModel { Id = 8, Description = "Eighth Task", IsCompleted = true },
            new ToDoDbModel { Id = 9, Description = "Nineth Task", IsCompleted = false },
            new ToDoDbModel { Id = 10, Description = "Tenth Task", IsCompleted = false },
            new ToDoDbModel { Id = 11, Description = "Eleventh Task", IsCompleted = true },
            new ToDoDbModel { Id = 12, Description = "Twelveth Task", IsCompleted = false }
        };

        private static readonly object[] sourceListForFiltering =
        {
            new object[] { 1, 4, 1, new FilterDto { BothFilter = null, DescriptionFilter = null, IsCompletedFilter = false } },
            new object[] { 2, 3, 0, new FilterDto { BothFilter = null, DescriptionFilter = null, IsCompletedFilter = true } },
            new object[] { 3, 2, 2, new FilterDto { BothFilter = true, DescriptionFilter = null, IsCompletedFilter = false } },
            new object[] { 4, 2, 2, new FilterDto { BothFilter = true, DescriptionFilter = null, IsCompletedFilter = true } },
            new object[] { 5, 4, 1, new FilterDto { BothFilter = false, DescriptionFilter = null, IsCompletedFilter = false } },
            new object[] { 6, 3, 0, new FilterDto { BothFilter = false, DescriptionFilter = null, IsCompletedFilter = true } },
            new object[] { 7, 2, 0, new FilterDto { BothFilter = null, DescriptionFilter = "F", IsCompletedFilter = false } },
            new object[] { 8, 1, 0, new FilterDto { BothFilter = null, DescriptionFilter = "F", IsCompletedFilter = true } },
            new object[] { 9, 3, 0, new FilterDto { BothFilter = true, DescriptionFilter = "F", IsCompletedFilter = false } },
            new object[] { 10, 3, 0, new FilterDto { BothFilter = true, DescriptionFilter = "F", IsCompletedFilter = true } },
            new object[] { 11, 2, 0, new FilterDto { BothFilter = false, DescriptionFilter = "F", IsCompletedFilter = false } },
            new object[] { 12, 1, 0, new FilterDto { BothFilter = false, DescriptionFilter = "F", IsCompletedFilter = true } },
        };

        [TestCase(TestName = "Get ToDo item by Id")]
        public async Task GetToDosFromContext()
        {
            using MsSqlLiteDatabaseContext context = new MsSqlLiteDatabaseContext(optionsSnapShotMock.Object);
            var toDoService = GetToDoService(context, toDosForContextLoading);
            var toDo = await toDoService.GetToDoItemByIdAsync(id: 2);

            Assert.IsNotNull(toDo);
            Assert.AreEqual(2, toDo.Id);
            Assert.AreEqual("Second Task", toDo.Description);
            Assert.IsFalse(toDo.IsCompleted);

            context.Database.CloseConnection();
        }

        [TestCase(TestName = "Multiple page test")]
        public async Task GetAllPagingTest()
        {
            using MsSqlLiteDatabaseContext context = new MsSqlLiteDatabaseContext(optionsSnapShotMock.Object);
            var toDoService = GetToDoService(context, toDosForPaging);
            var paging = new PagingDto { PageNumber = 2, PageSize = 5 };

            int expectedToDosCount = 2;
            int actualToDosCount = (await toDoService.GetAllAsync(null, paging)).Count();

            Assert.AreEqual(expectedToDosCount, actualToDosCount);

            context.Database.CloseConnection();
        }

        [Test(Description = "Filtering test"), TestCaseSource(nameof(sourceListForFiltering))]
        public async Task GetAllFilteringTest(int rowNumber, int expectedCount, int pageNumber, FilterDto filter)
        {
            using MsSqlLiteDatabaseContext context = new MsSqlLiteDatabaseContext(optionsSnapShotMock.Object);
            var toDoService = GetToDoService(context, toDosForFiltering);
            var paging = new PagingDto { PageNumber = pageNumber, PageSize = 5 };

            int actualCount = (await toDoService.GetAllAsync(filter, paging)).Count();

            Assert.AreEqual(expectedCount, actualCount);

            Debug.WriteLine($"TestCase row {rowNumber} completed.");

            context.Database.CloseConnection();
        }

        private ToDoService GetToDoService(MsSqlLiteDatabaseContext context, List<ToDoDbModel> toDos)
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.ToDos.AddRange(toDos);
            context.SaveChanges();

            var toDoRepository = new ToDoRepository(context, kernel.Get<IToDoEntityConverter>());
            return new ToDoService(toDoRepository);
        }
    }
}