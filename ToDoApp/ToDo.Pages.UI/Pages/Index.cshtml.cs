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
        private const int PageSize = 5;

        private readonly IToDoService toDoService;

        public IndexModel(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        public IEnumerable<ToDoDto> AllToDos { get; set; }

        [BindProperty]
        public int PageCount { get; set; }

        [BindProperty]
        public int CurrentPage { get; set; }

        public async Task<ActionResult> OnGet()
        {
            int currentPage = 0;
            var paging = new PagingDto
            {
                PageNumber = currentPage,
                PageSize = PageSize
            };
            await SetPageCount();

            AllToDos = await toDoService.GetAllAsync(GetFilter(), paging);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var paging = new PagingDto
            {
                PageNumber = CurrentPage,
                PageSize = PageSize
            };
            //await SetPageCount();
            AllToDos = await toDoService.GetAllAsync(GetFilter(), paging);
            return Page();
        }

        private FilterDto GetFilter() => new FilterDto { BothFilter = true };

        private async Task SetPageCount()
        {
            int recordCount = await toDoService.GetAllRecordCountAsync(GetFilter());
            PageCount = GetPageCount(recordCount, PageSize);
        }

        private int GetPageCount(int recordCount, int pageSize)
        {
            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize != 0)
            {
                pageCount += 1;
            }

            return pageCount;
        }
    }
}