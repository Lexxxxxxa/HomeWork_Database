using System;
using System.Collections.Generic;

namespace HomeWork_Database.Models;

public partial class LibraryStaff
{
    public int LibraryStaffId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? LibraryBranch { get; set; }

    public string? Password { get; set; }
}
