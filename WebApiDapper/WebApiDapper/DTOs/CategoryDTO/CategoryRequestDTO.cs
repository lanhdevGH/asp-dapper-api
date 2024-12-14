namespace WebApiDapper.DTOs.CategoryDTO
{
    public class CategoryRequestDTO
    {
        public string Name { get; set; }
        public string SeoAlias { get; set; } = "";
        public string SeoTitle { get; set; } = "";
        public string SeoKeyword { get; set; } = "";
        public string SeoDescription { get; set; } = "";
        public int ParentId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
