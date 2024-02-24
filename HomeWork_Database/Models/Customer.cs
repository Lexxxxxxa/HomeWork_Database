using System;
using System.Collections.Generic;

namespace HomeWork_Database.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public int? PurchaseQuantity { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
}
