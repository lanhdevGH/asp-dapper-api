using System.ComponentModel.DataAnnotations.Schema;
using WebApiDapper.Contracts;

namespace WebApiDapper.Entities
{
    [Table("Categories")]
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoAlias { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public string SeoDescription { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
