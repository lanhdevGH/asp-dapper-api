using System.ComponentModel.DataAnnotations;

namespace WebApiDapper.DTOs.ProductDTO
{
    public class ProductCreateRequestDTO
    {
        [Required(ErrorMessage = "Name is require")]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";

        [Required(ErrorMessage = "Sku is require")]
        public string Sku { get; set; }
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Required(ErrorMessage = "Image is require")]
        public string ImageUrl { get; set; }
        public string ImageList { get; set; } = "";
        public int CategoryId { get; set; }
        public Dictionary<string, string>? ExtendAttributes { get; set; }
    }
}
