using System;
using System.Collections.Generic;

namespace ClassLibrary_LitFilmHub;

public partial class Member
{
    public int MemberID { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleInitial { get; set; }

    public string? LastName { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    // Navigation property for Discussions
    public virtual ICollection<Discussion>? Discussions { get; set; }

}
