using System;
using System.Collections.Generic;

namespace ClassLibrary_LitFilmHub;

public partial class Book
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Genre { get; set; }

    public int? PublicationYear { get; set; }

    public string? Isbn { get; set; }

    public string? Summary { get; set; }

    public string? CoverImageUrl { get; set; }
}
