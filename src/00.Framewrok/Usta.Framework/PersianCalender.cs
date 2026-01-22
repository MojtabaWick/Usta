using System.Globalization;

namespace Usta.Framework
{
    public static class PersianCalendarExtensions
    {
        private static readonly PersianCalendar Pc = new PersianCalendar();

        public static DateTime ToGregorianDateTime(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                throw new ArgumentException("تاریخ شمسی نمی‌تواند خالی باشد.");

            // تبدیل اعداد فارسی به انگلیسی
            persianDate = persianDate
                .Replace("۰", "0").Replace("۱", "1").Replace("۲", "2")
                .Replace("۳", "3").Replace("۴", "4").Replace("۵", "5")
                .Replace("۶", "6").Replace("۷", "7").Replace("۸", "8")
                .Replace("۹", "9");

            // یکسان‌سازی جداکننده‌ها
            persianDate = persianDate.Replace("-", "/").Trim();

            var parts = persianDate.Split('/');
            if (parts.Length != 3)
                throw new FormatException("فرمت تاریخ شمسی باید YYYY/MM/DD باشد.");

            if (!int.TryParse(parts[0], out int year) ||
                !int.TryParse(parts[1], out int month) ||
                !int.TryParse(parts[2], out int day))
                throw new FormatException("سال، ماه یا روز معتبر نیست.");

            // اعتبارسنجی بازه شمسی
            if (year < 1300 || year > 1500 ||
                month < 1 || month > 12 ||
                day < 1 || day > 31)
                throw new ArgumentOutOfRangeException("تاریخ شمسی خارج از محدوده معتبر است.");

            try
            {
                // تبدیل دقیق شمسی به میلادی
                return Pc.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"تاریخ شمسی نامعتبر است: {persianDate}", ex);
            }
        }

        /// <summary>
        /// تبدیل DateTime میلادی به تاریخ شمسی (YYYY/MM/DD)
        /// </summary>
        public static string ToPersianDate(this DateTime dateTime)
        {
            int year = Pc.GetYear(dateTime);
            int month = Pc.GetMonth(dateTime);
            int day = Pc.GetDayOfMonth(dateTime);

            return $"{year:0000}/{month:00}/{day:00}";
        }
    }
}