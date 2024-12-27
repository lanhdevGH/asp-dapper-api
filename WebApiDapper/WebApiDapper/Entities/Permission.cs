using System.ComponentModel.DataAnnotations;

namespace WebApiDapper.Entities
{
    public class Permission
    {
        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public string FunctionId { get; set; } = string.Empty;

        [Required]
        public string ActionId { get; set; } = string.Empty;
    }
}
