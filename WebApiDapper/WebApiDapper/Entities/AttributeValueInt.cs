using System.ComponentModel.DataAnnotations;

namespace WebApiDapper.Entities
{
    public class AttributeValueInt
    {
        [Key]
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public int Value { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
