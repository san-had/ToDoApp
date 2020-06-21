using System;
using System.Collections.Generic;
using Ninject;
using ToDo.Domain;
using ToDo.Extensibility.Dto;
using ToDo.Service;

namespace ToDo.Con
{
    internal class Program
    {
        private static StandardKernel kernel = new StandardKernel();
        private static ToDoService toDoService;

        private static void Main(string[] args)
        {
            Init();

            var toDo = new ToDoDto
            {
                Description = "Eleventh ToDo"
            };
            Console.WriteLine(CreateToDoRecord(toDo));

            var filter = new FilterDto
            {
                //DescriptionFilter = "First",
                //IsCompletedFilter = true
            };

            var paging = new PagingDto
            {
                PageSize = 5,
                PageNumber = 1
            };

            DisplayToDos(GetAll(filter, paging));
            //toDoService.DeleteToDoItem(3);
            //DisplayToDo(toDoService.GetToDoItemById(3));

            //var toDo = toDoService.GetToDoItemById(1);
            //toDo.IsCompleted = true;
            //toDoService.UpdateToDoItem(toDo);
            //DisplayToDo(toDoService.GetToDoItemById(3));
            //DisplayToDos(GetAll());
        }

        private static void Init()
        {
            kernel.Load<ServiceNinjectModule>();
            kernel.Load<DomainNinjectModule>();
            toDoService = kernel.Get<ToDoService>();
        }

        private static int CreateToDoRecord(ToDoDto toDo)
        {
            return toDoService.CreateToDoItem(toDo);
        }

        private static IEnumerable<ToDoDto> GetAll(FilterDto filter, PagingDto paging)
        {
            return toDoService.GetAll(filter, paging);
        }

        private static void DisplayToDos(IEnumerable<ToDoDto> toDoDtos)
        {
            foreach (var toDo in toDoDtos)
            {
                string isCompleted = toDo.IsCompleted ? "Yes" : "No";
                Console.WriteLine($"Id: {toDo.Id}\tDescription: {toDo.Description}\tIsCompleted: {isCompleted}");
            }
        }

        private static void DisplayToDo(ToDoDto toDoDto)
        {
            if (toDoDto == null)
            {
                return;
            }
            string isCompleted = toDoDto.IsCompleted ? "Yes" : "No";
            Console.WriteLine($"Id: {toDoDto.Id}\tDescription: {toDoDto.Description}\tIsCompleted: {isCompleted}");
        }
    }
}