namespace WebApiDapper.Contracts
{
    public interface IEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
