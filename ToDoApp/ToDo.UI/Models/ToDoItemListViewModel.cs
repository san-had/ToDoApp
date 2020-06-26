using System.Collections.Generic;

namespace ToDo.UI.Models
{
    public class ToDoItemListViewModel
    {
        public List<ToDoItemViewModel> ToDoItemViewList { get; set; }

        public Pager PagerObj { get; set; }
    }
}