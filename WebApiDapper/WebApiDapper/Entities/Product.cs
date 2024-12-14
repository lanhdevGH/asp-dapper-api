using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiDapper.Contracts;

namespace WebApiDapper.Entities
{
    [Table("Products")]
    public class Product : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is require")]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";

        [Required(ErrorMessage = "Sku is require")]
        public string Sku {  get; set; }         
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Required(ErrorMessage = "Image is require")]
        public string ImageUrl { get; set; }
        public string ImageList { get; set; } = "";
        public int ViewCount { get; set; } = 0;

        public int RateTotal { get; set; } = 0;
        public int RateCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
