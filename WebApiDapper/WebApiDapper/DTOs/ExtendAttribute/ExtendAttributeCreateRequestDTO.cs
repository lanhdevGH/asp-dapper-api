using WebApiDapper.Enums;

namespace WebApiDapper.DTOs.ExtendAttribute
{
    public class ExtendAttributeCreateRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public AttributeDBType DataType { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
