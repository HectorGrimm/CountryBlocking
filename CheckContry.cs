namespace BlockCountry
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    /* Created by r3xq1
     * https://github.com/r3xq1
     * https://t.me/r3xq1
    */

    public static class CheckContry
    {
        // Список стран в которых нельзя работать приложению
        private static readonly List<string> ContryList = new List<string>
        {
            "Armenia", "Azerbaijan", "Belarus", "Kazakhstan", "Kyrgyzstan",
            "Moldova", "Tajikistan", "Uzbekistan", "Ukraine", "Russia"
        };
        public static void SaveCurrentCountry(bool allsave)
        {
            if (allsave)
            {
                var culturewrite = new StringBuilder();
                try
                {
                    CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
                    culturewrite.AppendLine("<table border=\"1\"><tr><th>Country Name</th><th>Language-Country code</th></tr>");
                    foreach (CultureInfo cul in cinfo)
                    {
                        culturewrite.AppendLine("<tr>");
                        culturewrite.AppendLine($"<td>{cul.DisplayName} </td><td> {cul.Name}</td>");
                        culturewrite.AppendLine("</tr>");
                    }
                    culturewrite.AppendLine("</table>");
                    File.WriteAllText("AllCulture.html", culturewrite?.ToString());
                }
                catch { }
            }
            else
            {
                try
                {
                    File.WriteAllText("CurrentCulture.txt", CultureInfo.CurrentCulture.ToString());
                }
                catch { }
            }
        }
        private static string Info
        {
            get
            {
                // Создаём пустую строку для получения страны из xml данных
                string country = string.Empty;
                try
                {
                    var doc = new XmlDocument();
                    // Загружаем xml
                    doc.LoadXml(GetSourceXml("https://ipapi.co/xml"));
                    // Получаем элемент имени страны в переменную country
                    country = doc.GetElementsByTagName("country_name")[0].InnerText;
                }
                catch { }
                return country; // Возвращаем строку с полученными данными
            }
        }
        private static string GetSourceXml(string url)
        {
            // Создаём пустую строку для получения xml данных
            string xmlStr = string.Empty;
            try
            {
                var link = new Uri(url, UriKind.Absolute);
                using (var wc = new WebClient())
                {
                    xmlStr = wc.DownloadString(link); // Загружаем данные по ссылке
                }
            }
            catch { }
            return xmlStr; // Возвращаем данные в виде строки
        }
        public static bool Inizialize()
        {
            ContryList.Sort(); // Сортируем список
            foreach (string list in ContryList) // Проходимся по списку стран
            {
                if (Info.Contains(list)) // Сверяем полученную страну из xml ссылки с нашим списком ContryList
                {
                    return true; // Возвращаем true - если нашли совпадение!
                }
            }
            return false; // Возвращаем false - если не нашли ничего!
        }
        public static bool OfflineInizialize()
        {
            ContryList.Sort(); // Сортируем список
            var ri = new RegionInfo(CultureInfo.CurrentCulture.Name);
            foreach (string list in ContryList)
            {
                if (list.Contains(ri.EnglishName))
                {
                    return true; // Возвращаем true - если нашли совпадение!
                }
            }
            return false; // Возвращаем false - если не нашли ничего!
        }

        public static bool Local()
        {
            try
            {
                string currentlanguage = CultureInfo.CurrentCulture?.ToString();
                var regionlanguage = new RegionInfo(currentlanguage);
                var localZone = TimeZoneInfo.Local;
                foreach (string country in ContryList)
                {
                    // Проверяем язык системы и часовой пояс
                    if (country.Contains(regionlanguage.EnglishName) || localZone.Id.Contains(country))
                    {
                        return true;
                    }
                    // тут доп можно сделать проверку через онлайн сервер и.т.п как хотите.
                }
            }
            catch (Exception) { }
            return false;
        }
    }
}
