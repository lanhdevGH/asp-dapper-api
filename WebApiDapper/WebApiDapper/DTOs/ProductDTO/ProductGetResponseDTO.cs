using WebApiDapper.Entities;

namespace WebApiDapper.DTOs.ProductDTO
{
    public class ProductGetResponseDTO : Product
    {
        public Dictionary<string, string>? ExtendAttributes { get; set; }
    }
}
