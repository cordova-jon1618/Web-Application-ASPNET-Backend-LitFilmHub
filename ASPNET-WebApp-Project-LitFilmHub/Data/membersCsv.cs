namespace ASPNET_WebApp_Project_LitFilmHub.Data
{
    public class MembersCsv
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleInitial { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
