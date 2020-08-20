using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Extensibility;
using ToDo.Extensibility.Dto;

namespace ToDo.Pages.UI.Pages
{
    public class FilterModel : PageModel
    {
        private readonly IToDoService todoService;

        public FilterModel(IToDoService todoService)
        {
            this.todoService = todoService;
        }

        [BindProperty]
        public FilterBindingModel Filter { get; set; }

        public IEnumerable<ToDoDto> FilteredToDos { get; set; }

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

                var paging = new PagingDto
                {
                    PageNumber = 0,
                    PageSize = 100
                };

                FilteredToDos = await todoService.GetAllAsync(filter, paging);
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