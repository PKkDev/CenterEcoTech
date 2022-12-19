namespace CenterEcoTech.Domain.DTO.User
{
    public class UserDetailDto
    {
        public string Phone { get; set; }

        public string? Email { get; set; }

        public string FirstName { get; set; }

        public string? LastNme { get; set; }

        public string? MidName { get; set; }

        public UserDetailAdressDto Adress { get; set; }
    }

    public class UserDetailAdressDto
    {
        public string? City { get; set; }

        public string? Street { get; set; }

        public string? House { get; set; }

        public string? Corpus { get; set; }

        public string? Room { get; set; }
    }
}
