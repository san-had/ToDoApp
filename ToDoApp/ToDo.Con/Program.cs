using System;
using Ninject;
using ToDo.Domain;
using ToDo.Extensibility.Dto;
using ToDo.Service;

namespace ToDo.Con
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var toDo = new ToDoDto
            {
                Description = "First ToDo",
                IsCompleted = false
            };
            Console.WriteLine(WriteToDoRecord(toDo));
        }

        private static int WriteToDoRecord(ToDoDto toDo)
        {
            var kernel = new StandardKernel();
            kernel.Load<ServiceNinjectModule>();
            kernel.Load<DomainNinjectModule>();
            var toDoService = kernel.Get<ToDoService>();

            return toDoService.CreateToDoItem(toDo);
        }
    }
}