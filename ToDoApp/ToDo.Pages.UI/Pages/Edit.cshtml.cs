using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;
using ToDo.Pages.UI.Models;

namespace ToDo.Pages.UI.Pages
{
    public class EditModel : PageModel
    {
        private readonly IToDoService toDoService;

        public EditModel(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        [BindProperty]
        public ToDoBindingModel ToDoItemEditModel { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var toDoDto = await toDoService.GetToDoItemByIdAsync(id);
            var toDoItem = new ToDoBindingModel
            {
                Id = toDoDto.Id,
                Description = toDoDto.Description,
                IsCompleted = toDoDto.IsCompleted
            };
            ToDoItemEditModel = toDoItem;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var toDoItem = new ToDoDto
                {
                    Id = ToDoItemEditModel.Id,
                    Description = ToDoItemEditModel.Description,
                    IsCompleted = ToDoItemEditModel.IsCompleted
                };
                await toDoService.UpdateToDoItemAsync(toDoItem);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}