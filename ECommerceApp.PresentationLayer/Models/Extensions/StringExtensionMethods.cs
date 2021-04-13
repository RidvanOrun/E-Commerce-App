using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Models.Extensions
{
    public static class StringExtensionMethods
    {
        public static string Slug(this string s)
        {
            var sb = new StringBuilder();
            //url'deki slug içerisinde karakter karakter dolaşıyoruz, o adımdaki karakter "-" yada herhangi bir noktalama işareti ise if bloğu devreye girecek
            foreach (char character in s) if (character == '-' || !char.IsPunctuation(character)) sb.Append(character);

            return sb.ToString().Replace(' ', '-').ToLower();
        }

        public static int ToInt(this string s)
        {
            int.TryParse(s, out int id);
            return id;
        }

        public static bool EqualsNoCase(this string s, string toCompare) => s?.ToLower() == toCompare?.ToLower();

        public static string Capitalize(this string s) => s?.Substring(0, 1)?.ToUpper() + s?.Substring(1);
    }
}
