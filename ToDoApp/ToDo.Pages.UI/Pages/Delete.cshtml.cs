using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Extensibility;
using ToDo.Pages.UI.Models;

namespace ToDo.Pages.UI.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IToDoService toDoService;

        public DeleteModel(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        [BindProperty]
        public ToDoBindingModel ToDoItemDeleteModel { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var toDoDto = await toDoService.GetToDoItemByIdAsync(id);
            var toDoItem = new ToDoBindingModel
            {
                Id = toDoDto.Id,
                Description = toDoDto.Description,
                IsCompleted = toDoDto.IsCompleted
            };
            ToDoItemDeleteModel = toDoItem;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await toDoService.DeleteToDoItemAsync(ToDoItemDeleteModel.Id);
            return RedirectToPage("Index");
        }
    }
}