using System;
using System.Collections.Generic;
using HomeWork_Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_Database;

public partial class LibraryStaffAndCustomersContext : DbContext
{
    public LibraryStaffAndCustomersContext()
    {
    }

    public LibraryStaffAndCustomersContext(DbContextOptions<LibraryStaffAndCustomersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookLoan> BookLoans { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<LibraryStaff> LibraryStaffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=WIN-S30M8Q5Q7QL;Initial Catalog=LibraryStaffAndCustomers;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__8BE5A10D88D898AD");

            entity.ToTable("Book");

            entity.Property(e => e.BookId)
                .ValueGeneratedNever()
                .HasColumnName("bookId");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genre");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BookLoan>(entity =>
        {
            entity.HasKey(e => e.BookLoanId).HasName("PK__BookLoan__2561E825AA82A8B5");

            entity.ToTable("BookLoan");

            entity.Property(e => e.BookLoanId).ValueGeneratedNever();
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.LoanDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.BookLoans)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookLoan__BookId__403A8C7D");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookLoans)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BookLoan__Custom__3F466844");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB7DD5C80C2D");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customerId");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.PurchaseQuantity).HasColumnName("purchaseQuantity");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.LibraryId).HasName("PK__Library__95E69EEED08D1F6C");

            entity.ToTable("Library");

            entity.Property(e => e.LibraryId)
                .ValueGeneratedNever()
                .HasColumnName("libraryId");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.NumberOfBooks).HasColumnName("numberOfBooks");
            entity.Property(e => e.NumberOfMembers).HasColumnName("numberOfMembers");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<LibraryStaff>(entity =>
        {
            entity.HasKey(e => e.LibraryStaffId).HasName("PK__LibraryS__D61CE24ADA0BF9EC");

            entity.ToTable("LibraryStaff");

            entity.Property(e => e.LibraryStaffId)
                .ValueGeneratedNever()
                .HasColumnName("libraryStaffId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LibraryBranch).HasColumnName("libraryBranch");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
