using System.Globalization;

namespace FinancialIQ.Code
{
    public static class Extensions
    {
        public static string ToTitleCase(this string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }

    }
}