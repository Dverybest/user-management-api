namespace TestApi.Models
{
    public partial class UserJobInfo
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
    

        public UserJobInfo()
        {
            JobTitle ??= "";
            Department ??= "";
        }
    }
}