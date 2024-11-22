using WebApiDapper.Contracts;

namespace WebApiDapper.Entities
{
    public class Language : IEntity
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
