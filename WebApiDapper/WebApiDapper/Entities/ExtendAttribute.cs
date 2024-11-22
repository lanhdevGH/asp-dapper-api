using System.ComponentModel.DataAnnotations.Schema;
using WebApiDapper.Contracts;
using WebApiDapper.Enums;

namespace WebApiDapper.Entities
{
    [Table("Attributes")]
    public class ExtendAttribute : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public AttributeDBType DataType { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
