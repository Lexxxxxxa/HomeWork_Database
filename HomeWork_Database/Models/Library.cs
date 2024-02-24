using System;
using System.Collections.Generic;

namespace HomeWork_Database.Models;

public partial class Library
{
    public int LibraryId { get; set; }

    public string? Location { get; set; }

    public string? PhoneNumber { get; set; }

    public int? NumberOfBooks { get; set; }

    public int? NumberOfMembers { get; set; }
}
