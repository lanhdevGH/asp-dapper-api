namespace WebApiDapper.DTOs.CategoryDTO
{
    public class CategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoAlias { get; set; } = "";
        public string SeoTitle { get; set; } = "";
        public string SeoKeyword { get; set; } = "";
        public string SeoDescription { get; set; } = "";
        public CategoryResponseDTO? ParentId { get; set; } = null;
        public bool IsActive { get; set; } = true;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
