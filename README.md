# CountryBlocking
Блокировка работы приложения в определённых странах

Работает как через онлайн: https://ipapi.co так и через [RegionInfo](https://csharp.net-tutorials.com/ru/411/культурные-региональные-особенности/класс-regioninfo/) и есть проверка по часовому поясу [TimeZoneInfo](https://docs.microsoft.com/ru-ru/dotnet/api/system.timezoneinfo?view=netcore-3.1)<br><br>
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
            if (CheckContry.Local())
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
