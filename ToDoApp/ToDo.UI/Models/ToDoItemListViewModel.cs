using System.Collections.Generic;

namespace ToDo.UI.Models
{
    public class ToDoItemListViewModel
    {
        public List<ToDoItemViewModel> ToDoItemViewList { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public string DescriptionFilter { get; set; }

        public bool IsCompletedFilter { get; set; }

        public bool? BothFilter { get; set; }
    }
}