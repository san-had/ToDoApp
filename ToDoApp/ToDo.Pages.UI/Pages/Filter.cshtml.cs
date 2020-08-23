using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Extensibility.Dto;
using ToDo.Pages.UI.Services;

namespace ToDo.Pages.UI.Pages
{
    public class FilterModel : PageModel
    {
        private readonly IPaginationService paginationService;

        public FilterModel(IPaginationService paginationService)
        {
            this.paginationService = paginationService;
        }

        [BindProperty]
        public FilterBindingModel Filter { get; set; }

        public IEnumerable<ToDoDto> FilteredToDos { get; set; }

        [BindProperty]
        public int PageCount { get; set; }

        [BindProperty]
        public int CurrentPage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var filter = new FilterDto
                {
                    DescriptionFilter = Filter.DescriptionFilter,
                    IsCompletedFilter = Filter.IsCompletedFilter,
                    BothFilter = Filter.BoothFilter
                };
                PageCount = await paginationService.GetPageCount(filter);
                if (PageCount < CurrentPage)
                {
                    CurrentPage = PageCount - 1;
                }
                FilteredToDos = await paginationService.GetTodos(filter, CurrentPage);
            }
            return Page();
        }

        public class FilterBindingModel
        {
            public string DescriptionFilter { get; set; }

            public bool IsCompletedFilter { get; set; }

            public bool BoothFilter { get; set; }
        }
    }
}