using System;
using System.Collections.Generic;

namespace HomeWork_Database.Models;

public partial class BookLoan
{
    public int BookLoanId { get; set; }

    public int? CustomerId { get; set; }

    public int? BookId { get; set; }

    public DateTime? LoanDate { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Customer? Customer { get; set; }
}
