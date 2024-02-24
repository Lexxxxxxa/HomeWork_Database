using HomeWork_Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Database.LibraryManagment
{
    public class LibraryManager
    {
        static LibraryStaffAndCustomersContext context = new LibraryStaffAndCustomersContext();

        private readonly LibraryStaffAndCustomersContext _dbContext;

        public LibraryManager(LibraryStaffAndCustomersContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public static void DisplayBooks()
        {
            var books = context.Books.ToList();

            Console.WriteLine("Список книг:");
            foreach (var book in books)
            {
                Console.WriteLine($"Назва книги: {book.Name}, опис: {book.Description}, жанр{book.Genre}");
                Console.WriteLine();
            }
        }

        public static void AddNewBook()
        {
            Console.WriteLine("Введіть назву нової книги:");
            string bookName = Console.ReadLine();

            Console.WriteLine("Введіть назву опис книги:");
            string bookDescription = Console.ReadLine();

            Console.WriteLine("Введіть назву жанр книги:");
            string bookGenre = Console.ReadLine();

            context.Books.Add(new Book { Name = bookName, Description = bookDescription, Genre = bookGenre});
            context.SaveChanges();

            Console.WriteLine("Нова книга додана до бази даних.");
        }


        public void AddReader(string name, string lastName, string phone)
        {
            var newReader = new Customer
            {
                Name = name,
                LastName = lastName,
                Phone = phone
            };

            _dbContext.Customers.Add(newReader);
            _dbContext.SaveChanges();
            Console.WriteLine($"Читач {name} {lastName} успішно доданий.");
        }

        public void UpdateReader(int readerId, string newName, string newLastName, string newPhone)
        {
            var readerToUpdate = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == readerId);

            if (readerToUpdate != null)
            {
                readerToUpdate.Name = newName;
                readerToUpdate.LastName = newLastName;
                readerToUpdate.Phone = newPhone;

                _dbContext.SaveChanges();
                Console.WriteLine($"Дані читача з id {readerId} успішно оновлені.");
            }
            else
            {
                Console.WriteLine($"Читача з id {readerId} не знайдено.");
            }
        }

        public void DeleteReader(int readerId)
        {
            var readerToDelete = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == readerId);

            if (readerToDelete != null)
            {
                _dbContext.Customers.Remove(readerToDelete);
                _dbContext.SaveChanges();
                Console.WriteLine($"Читач з id {readerId} успішно видалений.");
            }
            else
            {
                Console.WriteLine($"Читача з id {readerId} не знайдено.");
            }
        }
        public void DisplayReadersHistory()
        {
            var readers = _dbContext.Customers.Include(c => c.BookLoans).ToList();

            foreach (var reader in readers)
            {
                Console.WriteLine($"Читач: {reader.Name} {reader.LastName}");
                Console.WriteLine("Історія позичень:");

                foreach (var loan in reader.BookLoans)
                {
                    Console.WriteLine($"Книга: {loan.Book.Name}, Дата позики: {loan.LoanDate}, Дата повернення: {loan.ReturnDate}");
                }

                Console.WriteLine();
            }
        }
        public void DisplayReaderBookHistory(int readerId)
        {
            var reader = _dbContext.Customers.Include(c => c.BookLoans).FirstOrDefault(c => c.CustomerId == readerId);

            if (reader != null)
            {
                Console.WriteLine($"Історія позичень для читача {reader.Name} {reader.LastName}:");

                foreach (var loan in reader.BookLoans)
                {
                    Console.WriteLine($"Книга: {loan.Book.Name}, Дата позики: {loan.LoanDate}, Дата повернення: {loan.ReturnDate}");
                }
            }
            else
            {
                Console.WriteLine("Читача з вказаним ідентифікатором не знайдено.");
            }
        }

        public void TakeBook(int readerId, string bookName)
        {
            var reader = _dbContext.Customers.FirstOrDefault(r => r.CustomerId == readerId);
            var book = _dbContext.Books.FirstOrDefault(b => b.Name.Equals(bookName, StringComparison.OrdinalIgnoreCase));

            if (reader != null && book != null)
            {
                var loan = new BookLoan
                {
                    CustomerId = readerId,
                    BookId = book.BookId,
                    LoanDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(30)
                };

                _dbContext.BookLoans.Add(loan);
                _dbContext.SaveChanges();
                Console.WriteLine($"Книга \"{book.Name}\" була успішно взята на позику користувачем {reader.Name} {reader.LastName}.");
            }
            else
            {
                Console.WriteLine("Не вдалося знайти користувача або книгу з вказаними даними.");
            }
        }

    }
}
