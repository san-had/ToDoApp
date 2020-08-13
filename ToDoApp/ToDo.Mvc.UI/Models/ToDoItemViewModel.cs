using System.ComponentModel.DataAnnotations;

namespace ToDo.UI.Models
{
    public class ToDoItemViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Task description")]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}