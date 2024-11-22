using System.ComponentModel.DataAnnotations.Schema;
using WebApiDapper.Contracts;

namespace WebApiDapper.Entities
{
    [Table("OrderDetails")]
    public class OrderDetail : IEntity
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
