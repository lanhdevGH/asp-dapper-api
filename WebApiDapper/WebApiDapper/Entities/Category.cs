namespace WebApiDapper.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoAlias { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public string SeoDescription { get; set; }
        public int ParentId { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
