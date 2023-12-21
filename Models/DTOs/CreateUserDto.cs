namespace Proiect.Models.DTOs
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
