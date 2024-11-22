using System.ComponentModel.DataAnnotations;
using WebApiDapper.Contracts;

namespace WebApiDapper.Entities
{
    public class AttributeValueNVarchar : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
