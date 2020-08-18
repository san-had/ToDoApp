using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;
using ToDo.Pages.UI.Models;

namespace ToDo.Pages.UI.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IToDoService toDoService;

        public CreateModel(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        [BindProperty]
        public ToDoBindingModel ToDoItemCreateModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var toDoItem = new ToDoDto
                {
                    Description = ToDoItemCreateModel.Description,
                    IsCompleted = ToDoItemCreateModel.IsCompleted
                };
                await toDoService.CreateToDoItemAsync(toDoItem);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}