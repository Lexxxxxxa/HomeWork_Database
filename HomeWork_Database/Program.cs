using HomeWork_Database.AuthAndReg;
using HomeWork_Database.Models;

namespace HomeWork_Database
{
    internal class Program
    {
        //dotnet ef dbcontext scaffold "Data Source=WIN-S30M8Q5Q7QL;Initial Catalog=LibraryStaffAndCustomers;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Вхід в систему");
                Console.WriteLine("2. Реєстрація нового бібліотекаря");
                Console.WriteLine("3. Реєстрація нового читача");
                Console.WriteLine("4. Вихід");
                Console.Write("Виберіть опцію: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Login();
                            break;
                        case 2:
                            RegisterLibrarian();
                            break;
                        case 3:
                            RegisterReader();
                            break;
                        case 4:
                            Console.WriteLine("Дякую за використання нашої системи. До побачення!");
                            return;
                        default:
                            Console.WriteLine("Невірний вибір опції. Спробуйте ще раз.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Невірний ввід. Будь ласка, введіть ціле число.");
                }

                Console.WriteLine();
            }
        }

        public static void Login()
        {
            var librarians = new List<LibraryStaff>();
            var readers = new List<Customer>();

            Console.WriteLine("Введіть ім'я користувача: ");
            string username = Console.ReadLine();

            Console.WriteLine("Введіть пароль: ");
            string password = Console.ReadLine();

            var authenticationManager = new AuthenticationManager(librarians, readers);
            bool isLoggedIn = authenticationManager.Login(username, password);

            Console.WriteLine("Ви увійшли в систему.");
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
    }
}
