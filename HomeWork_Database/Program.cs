using System.Collections.Generic;
using HomeWork_Database.Models;
using HomeWork_Database.AuthAndReg;
using HomeWork_Database.LibraryManagment;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace HomeWork_Database
{
    internal class Program
    {
        static AuthenticationManager authenticationManager = new AuthenticationManager(new List<LibraryStaff>(), new List<Customer>());
        private static LibraryStaffAndCustomersContext _dbContext;

        static void Main(string[] args)
        {
            bool isLoggedIn = false;
            bool isLibrarian = false;
            var manager = new LibraryManager(_dbContext);

            while (true)
            {
                if (!isLoggedIn)
                {
                    Console.WriteLine("Вхід у систему:");
                    Console.Write("Логін: ");
                    string username = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();

                    isLoggedIn = authenticationManager.Login(username, password);
                    isLibrarian = authenticationManager.IsLibrarian(username);

                    if (!isLoggedIn)
                    {
                        Console.WriteLine("Бажаєте зареєструватися?");
                        Console.WriteLine("1. Так, я новий користувач.");
                        Console.WriteLine("2. Ні, спробую ще раз.");
                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Оберіть тип користувача:");
                                Console.WriteLine("1. Бібліотекар.");
                                Console.WriteLine("2. Читач.");
                                int userTypeChoice = Convert.ToInt32(Console.ReadLine());

                                switch (userTypeChoice)
                                {
                                    case 1:
                                        RegisterLibrarian();
                                        break;
                                    case 2:
                                        RegisterReader();
                                        break;
                                    default:
                                        Console.WriteLine("Неправильний вибір.");
                                        break;
                                }
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір.");
                                break;
                        }
                    }
                }

                if (isLoggedIn)
                {
                    if (isLibrarian)
                    {
                        Console.WriteLine("Ви увійшли як бібліотекар.");
                        Console.WriteLine("Оберіть дію:");
                        Console.WriteLine("1. Переглянути всі книги.");
                        Console.WriteLine("2. Додати/оновити книги.");
                        Console.WriteLine("3. Додати/змінити/видалити читачів.");
                        Console.WriteLine("4. Переглянути історію та актуальну інформацію всіх читачів.");
                        Console.WriteLine("5. Вийти з системи.");
                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                LibraryManager.DisplayBooks();
                                break;
                            case 2:
                                LibraryManager.AddNewBook();
                                break;
                            case 3:
                                ManageReaders();
                                break;
                            case 4:
                                manager.DisplayReadersHistory();
                                break;
                            case 5:
                                isLoggedIn = false;
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ви увійшли як читач.");
                        Console.WriteLine("Оберіть дію:");
                        Console.WriteLine("1. Переглянути доступні книги.");
                        Console.WriteLine("2. Переглянути історію позичених книг.");
                        Console.WriteLine("3. Взяти книгу.");
                        Console.WriteLine("4. Вийти з системи.");
                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                LibraryManager.DisplayBooks();
                                break;
                            case 2:
                                Console.Write("Введіть логін: ");
                                string username = Console.ReadLine();
                                int loggedInUserId = (int)authenticationManager.GetUserId(username);
                                manager.DisplayReaderBookHistory(loggedInUserId);
                                break;
                            case 3:
                                Console.WriteLine("Введіть ідентифікатор читача:");
                                int readerId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Введіть назву книги:");
                                string bookName = Console.ReadLine();

                                manager.TakeBook(readerId, bookName);
                                break;
                            case 4:
                                isLoggedIn = false;
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Неправильний логін або пароль. Спробуйте ще раз.");
                }
            }
        }

        public static void RegisterLibrarian()
        {
            Console.WriteLine("Реєстрація нового бібліотекаря");
            Console.WriteLine("Введіть ім'я: ");
            string name = Console.ReadLine();

            Console.WriteLine("Введіть електронну пошту: ");
            string email = Console.ReadLine();

            Console.WriteLine("Введіть телефон: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Введіть назву відділення бібліотеки: ");
            int libraryBranch = int.Parse(Console.ReadLine());

            RegistrationManager registrationManager = new RegistrationManager();
            registrationManager.RegisterLibrarian(name, email, phone, libraryBranch);

            Console.WriteLine($"Бібліотекар {name} зареєстрований.");
        }

        public static void RegisterReader()
        {
            Console.WriteLine("Реєстрація нового читача");
            Console.WriteLine("Введіть ім'я: ");
            string name = Console.ReadLine();

            Console.WriteLine("Введіть прізвище: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Введіть телефон: ");
            string phone = Console.ReadLine();

            RegistrationManager registrationManager = new RegistrationManager();
            registrationManager.RegisterReader(name, lastName, phone);

            Console.WriteLine($"Читач {name} {lastName} зареєстрований.");
        }

        public static void ManageReaders()
        {
            var manager = new LibraryManager(_dbContext);

            Console.WriteLine("Оберіть опцію:");
            Console.WriteLine("1. Додати нового читача.");
            Console.WriteLine("2. Змінити дані читача.");
            Console.WriteLine("3. Видалити читача.");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введіть ім'я нового читача: ");
                    string newName = Console.ReadLine();
                    Console.WriteLine("Введіть прізвище нового читача: ");
                    string newLastName = Console.ReadLine();
                    Console.WriteLine("Введіть телефон нового читача: ");
                    string newPhone = Console.ReadLine();
                    manager.AddReader(newName, newLastName, newPhone);
                    break;
                case 2:
                    Console.WriteLine("Введіть ID читача, якого потрібно змінити: ");
                    int readerId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введіть нове ім'я читача: ");
                    string updatedName = Console.ReadLine();
                    Console.WriteLine("Введіть нове прізвище читача: ");
                    string updatedLastName = Console.ReadLine();
                    Console.WriteLine("Введіть новий телефон читача: ");
                    string updatedPhone = Console.ReadLine();
                    manager.UpdateReader(readerId, updatedName, updatedLastName, updatedPhone);
                    break;
                case 3:
                    Console.WriteLine("Введіть ID читача, якого потрібно видалити:");
                    int readerIdToDel = Convert.ToInt32(Console.ReadLine());
                    manager.DeleteReader(readerIdToDel);
                    break;
                default:
                    Console.WriteLine("Неправильний вибір.");
                    break;
            }
        }
    }
}
