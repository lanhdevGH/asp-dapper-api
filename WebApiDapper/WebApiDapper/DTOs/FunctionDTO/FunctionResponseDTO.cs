namespace WebApiDapper.DTOs.FunctionDTO
{
    public class FunctionResponseDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ParentId { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public string CssClass { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
