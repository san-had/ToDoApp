using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Converters;
using ToDo.Domain.Database.Providers;
using ToDo.Domain.Repositories;
using ToDo.Extensibility;
using ToDo.Mvc.UI.Converters;
using ToDo.Mvc.UI.Services;
using ToDo.Service;

namespace ToDo.Mvc.UI
{
    public static class ToDoServiceCollectionExtensions
    {
        public static IServiceCollection AddToDo(this IServiceCollection services)
        {
            services.AddScoped<MsSqlLiteDatabaseContext>();
            services.AddScoped<IToDoEntityConverter, ToDoEntityConverter>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<IToDoConverter, ToDoConverter>();
            services.AddScoped<IViewModelService, ViewModelService>();
            return services;
        }
    }
}