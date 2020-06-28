namespace ToDo.Extensibility.Dto
{
    public class FilterDto
    {
        public string DescriptionFilter { get; set; }

        public bool IsCompletedFilter { get; set; }

        public bool? BothFilter { get; set; }
    }
}