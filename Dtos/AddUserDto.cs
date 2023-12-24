namespace TestApi.Dtos
{
    public partial class AddUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Gender { get; set; }

        public AddUserDto()
        {
            FirstName ??= "";
            LastName ??= "";
            Gender ??= "";
            Email ??= "";
        }
    }
}