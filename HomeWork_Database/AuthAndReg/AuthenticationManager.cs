using HomeWork_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Database.AuthAndReg
{
    public class AuthenticationManager
    {
        private readonly List<LibraryStaff> librarians;
        private readonly List<Customer> readers;

        public AuthenticationManager(List<LibraryStaff> librarians, List<Customer> readers)
        {
            this.librarians = librarians;
            this.readers = readers;
        }

        public bool Login(string username, string password)
        {
            foreach (var librarian in librarians)
            {
                if (librarian.Email == username && librarian.Password == password)
                {
                    Console.WriteLine($"Ви увійшли як бібліотекар {librarian.Name}.");
                    return true;
                }
            }

            foreach (var reader in readers)
            {
                if (reader.Phone == username && reader.Password == password)
                {
                    Console.WriteLine($"Ви увійшли як читач {reader.Name} {reader.LastName}.");
                    return true;
                }
            }

            Console.WriteLine("Неправильне ім'я користувача або пароль.");
            return false;
        }
    }
}
