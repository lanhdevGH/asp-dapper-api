namespace WebApiDapper.Entities
{
    public class OrderDetail
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
