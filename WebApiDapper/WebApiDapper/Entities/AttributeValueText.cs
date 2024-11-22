using System;
using WebApiDapper.Contracts;

namespace WebApiDapper.Entities
{
    public class AttributeValueText : IEntity
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
