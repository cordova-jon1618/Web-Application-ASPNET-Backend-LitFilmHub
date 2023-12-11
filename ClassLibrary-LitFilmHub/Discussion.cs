using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_LitFilmHub;

public partial class Discussion
{
    public int DiscussionID { get; set; }
    public int MemberID { get; set; } // Foreign Key
    public string? Content { get; set; }

    // Navigation property for Member
    public virtual Member? Member { get; set; }
}