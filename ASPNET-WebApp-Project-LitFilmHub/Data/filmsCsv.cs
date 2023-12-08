namespace ASPNET_WebApp_Project_LitFilmHub.Data
{
    public class filmsCsv
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int? ReleaseYear { get; set; }
        public string Rating { get; set; } = null!;
        public string Synopsis { get; set; } = null!;
        public string PosterImageUrl { get; set; } = null!;

    }
}
