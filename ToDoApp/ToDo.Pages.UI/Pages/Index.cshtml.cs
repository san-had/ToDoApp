using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Extensibility.Dto;
using ToDo.Pages.UI.Services;

namespace ToDo.Pages.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPaginationService paginationService;

        public IndexModel(IPaginationService paginationService)
        {
            this.paginationService = paginationService;
        }

        public IEnumerable<ToDoDto> AllToDos { get; set; }

        [BindProperty]
        public int PageCount { get; set; }

        [BindProperty]
        public int CurrentPage { get; set; }

        public async Task<ActionResult> OnGet()
        {
            int currentPage = 0;
            var filter = GetFilter();
            PageCount = await paginationService.GetPageCount(filter);
            AllToDos = await paginationService.GetTodos(filter, currentPage);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            AllToDos = await paginationService.GetTodos(GetFilter(), CurrentPage);
            return Page();
        }

        private FilterDto GetFilter() => new FilterDto { BothFilter = true };
    }
}