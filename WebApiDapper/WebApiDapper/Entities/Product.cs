using System.ComponentModel.DataAnnotations;
using WebApiDapper.Contracts;

namespace WebApiDapper.Entities
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is require")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        [Required(ErrorMessage = "Sku is require")]
        public string Sku {  get; set; }

        [Required(ErrorMessage = "Image is require")]
        public string ImageUrl { get; set; }
        public string ImageList { get; set; }
        public int ViewCount { get; set; }
        public string SeoAlias { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public string SeoDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int RateTotal { get; set; }
        public int RateCount { get; set; }
        
    }
}
