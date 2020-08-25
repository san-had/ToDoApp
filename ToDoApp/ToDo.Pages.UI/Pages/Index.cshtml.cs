using System.Collections.Generic;
using System.Linq;
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

        public IList<ToDoDto> AllToDos { get; set; }

        [BindProperty]
        public int PageSize { get; set; }

        [BindProperty]
        public int CurrentPage { get; set; }

        [BindProperty]
        public int RecordCount { get; set; }

        public async Task<ActionResult> OnGet()
        {
            CurrentPage = 0;
            return await GetData();
        }

        public async Task<IActionResult> OnPost()
        {
            return await GetData();
        }

        private FilterDto GetFilter() => new FilterDto { BothFilter = true };

        private async Task<ActionResult> GetData()
        {
            PageSize = paginationService.PageSize;
            AllToDos = await paginationService.GetTodos(GetFilter(), CurrentPage);
            RecordCount = AllToDos.Count();
            return Page();
        }
    }
}