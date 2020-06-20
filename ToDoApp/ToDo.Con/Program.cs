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

            //var toDo = new ToDoDto
            //{
            //    Description = "Fifth ToDo"
            //};
            //Console.WriteLine(CreateToDoRecord(toDo));

            //DisplayToDos(GetAll());
            DisplayToDo(toDoService.GetToDoItemById(3));

            var toDo = toDoService.GetToDoItemById(3);
            toDo.IsCompleted = false;
            toDoService.UpdateToDoItem(toDo);
            DisplayToDo(toDoService.GetToDoItemById(3));
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

        private static IEnumerable<ToDoDto> GetAll()
        {
            return toDoService.GetAll();
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