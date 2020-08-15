using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;

namespace ToDo.Pages.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IToDoService toDoService;

        public IEnumerable<ToDoDto> AllToDos { get; set; }

        public IndexModel(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        public async Task<ActionResult> OnGet()
        {
            var filter = new FilterDto
            {
                BothFilter = true
            };
            var paging = new PagingDto
            {
                PageNumber = 0,
                PageSize = 100
            };

            AllToDos = await toDoService.GetAllAsync(filter, paging);
            return Page();
        }
    }
}