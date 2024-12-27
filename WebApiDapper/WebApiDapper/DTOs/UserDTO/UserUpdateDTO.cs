using System.ComponentModel.DataAnnotations;

namespace WebApiDapper.DTOs.UserDTO
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "Bắc buộc nhập")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bắc buộc nhập")]
        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bắc buộc nhập")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
