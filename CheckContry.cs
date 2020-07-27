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
                // ������ ������ ������ ��� ��������� ������ �� xml ������
                string country = string.Empty;
                try
                {
                    var doc = new XmlDocument();
                    // ��������� xml
                    doc.LoadXml(GetSourceXml("https://ipapi.co/xml"));
                    // �������� ������� ����� ������ � ���������� country
                    country = doc.GetElementsByTagName("country_name")[0].InnerText;
                }
                catch { }
                return country; // ���������� ������ � ����������� �������
            }
        }
        private static string GetSourceXml(string url)
        {
            // ������ ������ ������ ��� ��������� xml ������
            string xmlStr = string.Empty;
            try
            {
                var link = new Uri(url, UriKind.Absolute);
                using (var wc = new WebClient())
                {
                    xmlStr = wc.DownloadString(link); // ��������� ������ �� ������
                }
            }
            catch { }
            return xmlStr; // ��������� ������ � ���� ������
        }
        public static bool Inizialize()
        {
            // ������ ����� � ������� ������ �������� ����������
            var ContryList = new List<string>
            {
                "Armenia", "Azerbaijan", "Belarus", "Kazakhstan", "Kyrgyzstan",
                "Moldova", "Tajikistan", "Uzbekistan", "Ukraine", "Russia"
            };
            ContryList.Sort(); // ��������� ������
            foreach (string list in ContryList) // ���������� �� ������ �����
            {
                if (Info.Contains(list)) // ������� ���������� ������ �� xml ������ � ����� ������� ContryList
                {
                    return true; // ���������� true - ���� ����� ����������!
                }
            }
            return false; // ��������� false - ���� �� ����� ������! 
        }
        public static bool OfflineInizialize()
        {
            // ������ ����� � ������� ������ �������� ����������
            var ContryList = new List<string>
            {
                "Armenia", "Azerbaijan", "Belarus", "Kazakhstan", "Kyrgyzstan",
                "Moldova", "Tajikistan", "Uzbekistan", "Ukraine", "Russia"
            };
            try
            {
                ContryList.Sort(); // ��������� ������
                var ri = new RegionInfo(CultureInfo.CurrentCulture.Name);
                foreach (string list in ContryList)  
                {
                    if (list.Contains(ri.EnglishName))
                    {
                        return true; // ���������� true - ���� ����� ����������!
                    }
                }
            }
            catch (Exception ex) { File.WriteAllText("ErrorCulture.txt", $"{ex.Message}\r\n"); }
            return false; // ��������� false - ���� �� ����� ������! 
        }
    }
}