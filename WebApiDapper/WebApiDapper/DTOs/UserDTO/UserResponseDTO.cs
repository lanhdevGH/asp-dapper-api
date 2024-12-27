namespace WebApiDapper.DTOs.UserDTO
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public string Adrress { get; set; }
    }
}
