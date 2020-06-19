using Ninject.Modules;
using ToDo.Extensibility;

namespace ToDo.Service
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IToDoService>().To<ToDoService>();
        }
    }
}