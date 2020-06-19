using Ninject.Modules;
using ToDo.Domain.Converters;
using ToDo.Domain.Repositories;
using ToDo.Extensibility;

namespace ToDo.Domain
{
    public class DomainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IToDoEntityConverter>().To<ToDoEntityConverter>();
            Bind<IToDoRepository>().To<ToDoRepository>();
        }
    }
}