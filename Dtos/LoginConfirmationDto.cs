namespace TestApi.Dtos
{
    public partial class LoginConfirmationDto
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public LoginConfirmationDto()
        {
            PasswordHash ??= [];
            PasswordSalt ??= [];
        }
    }
}
