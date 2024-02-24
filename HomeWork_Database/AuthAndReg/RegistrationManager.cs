using HomeWork_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Database.AuthAndReg
{
    public class RegistrationManager
    {
        private readonly List<LibraryStaff> librarians = new List<LibraryStaff>();
        private readonly List<Customer> readers = new List<Customer>();

        public void RegisterLibrarian(string name, string email, string phone, int libraryBranch)
        {
            var librarian = new LibraryStaff
            {
                Name = name,
                Email = email,
                Phone = phone,
                LibraryBranch = libraryBranch
            };
            librarians.Add(librarian);
            Console.WriteLine($"Бібліотекар {name} зареєстрований.");
        }

        public void RegisterReader(string name, string lastName, string phone)
        {
            var reader = new Customer
            {
                Name = name,
                LastName = lastName,
                Phone = phone
            };
            readers.Add(reader);
            Console.WriteLine($"Читач {name} {lastName} зареєстрований.");
        }
    }
}
