using System;
using System.Collections.Generic;

namespace ClassLibrary_LitFilmHub;

public partial class Film
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Director { get; set; }

    public string? Genre { get; set; }

    public int? ReleaseYear { get; set; }

    public string? Rating { get; set; }

    public string? Synopsis { get; set; }

    public string? PosterImageUrl { get; set; }
}
