# CountryBlocking
Блокировка работы в определённых странах

Использовать так:
````csharp
namespace BlockCountry
{
    using System;

    internal static class Program
    {
        public static void Main()
        {
            Console.Title = "Block Country ";
            if (CheckContry.Inizialize()) // OfflineInizialize
            {
                Console.WriteLine("Мы не работаем в этой стране");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Добро пожаловать! В этой стране мы можем работать)");
                // Тут пишем вызовы методов, ваш код.
            }
            Console.Read();
        }
    }
}
````
