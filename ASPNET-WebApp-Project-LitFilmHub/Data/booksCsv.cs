namespace ASPNET_WebApp_Project_LitFilmHub.Data
{
    public class booksCsv
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int? PublicationYear { get; set; }
        public string Isbn { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;

    }
}
