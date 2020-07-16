using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private IOptionsSnapshot<ConfigurationSettings> fakeOptionsSnapShot;

        [SetUp]
        public void TestInitialize()
        {
            kernel.Load<ServiceNinjectModule>();
            kernel.Load<DomainNinjectModule>();
            kernel.Bind<IOptionsSnapshot<ConfigurationSettings>>().To<FakeOptionsSnapShot>();

            fakeOptionsSnapShot = kernel.Get<IOptionsSnapshot<ConfigurationSettings>>();
        }

        [TestCase]
        public void GetToDosFromContext()
        {
            using (var context = new MsSqlLiteDatabaseContext(fakeOptionsSnapShot))
            {
                context.Database.OpenConnection();

                bool isDbExists = context.Database.EnsureCreated();

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

    internal class FakeOptionsSnapShot : IOptionsSnapshot<ConfigurationSettings>
    {
        public ConfigurationSettings Value => new ConfigurationSettings
        {
            SqlLiteDbFilePath = ":memory:"
        };

        public ConfigurationSettings Get(string name)
        {
            return new ConfigurationSettings
            {
                SqlLiteDbFilePath = ":memory:"
            };
        }
    }
}