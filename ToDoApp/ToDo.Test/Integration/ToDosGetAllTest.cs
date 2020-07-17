using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Ninject;
using NUnit.Framework;
using ToDo.Domain;
using ToDo.Domain.Converters;
using ToDo.Domain.Database.Providers;
using ToDo.Domain.Repositories;
using ToDo.Extensibility.Dto;
using ToDo.Service;

namespace ToDo.Test.Integration
{
    public class ToDosGetAllTest
    {
        private StandardKernel kernel = new StandardKernel();

        private Mock<IOptionsSnapshot<ConfigurationSettings>> optionsSnapShotMock;

        [SetUp]
        public void TestInitialize()
        {
            kernel.Load<ServiceNinjectModule>();
            kernel.Load<DomainNinjectModule>();

            optionsSnapShotMock = new Mock<IOptionsSnapshot<ConfigurationSettings>>();
            kernel.Bind<IOptionsSnapshot<ConfigurationSettings>>().ToConstant(optionsSnapShotMock.Object);
            optionsSnapShotMock.SetupGet(m => m.Value).Returns(new ConfigurationSettings { SqlLiteDbFilePath = ":memory:" });
        }

        [TestCase(TestName = "Get ToDo item by Id")]
        public void GetToDosFromContext()
        {
            using (var context = new MsSqlLiteDatabaseContext(optionsSnapShotMock.Object))
            {
                context.Database.OpenConnection();

                context.Database.EnsureCreated();

                context.ToDos.AddRange(
                    new Domain.Database.Model.ToDoDbModel { Id = 1, Description = "First Task", IsCompleted = false },
                    new Domain.Database.Model.ToDoDbModel { Id = 2, Description = "Second Task", IsCompleted = false },
                    new Domain.Database.Model.ToDoDbModel { Id = 3, Description = "Third Task", IsCompleted = false }
                    );
                context.SaveChanges();

                var toDoRepository = new ToDoRepository(context, kernel.Get<IToDoEntityConverter>());

                var toDoService = new ToDoService(toDoRepository);

                var toDo = toDoService.GetToDoItemById(id: 2);

                Assert.IsNotNull(toDo);
                Assert.AreEqual(2, toDo.Id);
                Assert.AreEqual("Second Task", toDo.Description);
                Assert.IsFalse(toDo.IsCompleted);

                context.Database.CloseConnection();
            }
        }
    }
}