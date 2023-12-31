namespace TestApi.Dtos
{
    public partial class UserRegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRegistrationDto()
        {
            Email ??= "";
            Password ??= "";
        }
    }
}