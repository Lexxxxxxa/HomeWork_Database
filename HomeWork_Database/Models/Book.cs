using System;
using System.Collections.Generic;

namespace HomeWork_Database.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Genre { get; set; }

    public virtual ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
}
