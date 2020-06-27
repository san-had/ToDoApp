using System.Collections.Generic;

namespace ToDo.UI.Models
{
    public class ToDoItemListViewModel
    {
        public List<ToDoItemViewModel> ToDoItemViewList { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }
    }
}