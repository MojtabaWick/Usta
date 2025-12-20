using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Usta.Framework
{
    public static class PersianCalender
    {
        private static readonly PersianCalendar Pc = new PersianCalendar();

        /// <summary>
        /// تبدیل تاریخ شمسی (مثل 1403/11/30 یا ۱۴۰۳-۱۱-۳۰) به DateOnly میلادی
        /// با پشتیبانی از اعداد فارسی و انگلیسی
        /// </summary>
        public static DateOnly ToGregorianDateOnly(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                throw new ArgumentException("تاریخ شمسی نمی‌تواند خالی باشد.");

            // تبدیل اعداد فارسی به انگلیسی (حتی اگر کاربر دستی تایپ کرده باشه)
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

            // چک کردن محدوده معتبر تاریخ شمسی
            if (year < 1300 || year > 1500 || month < 1 || month > 12 || day < 1 || day > 31)
                throw new ArgumentOutOfRangeException("تاریخ شمسی خارج از محدوده معتبر است.");

            try
            {
                // مستقیم از DateOnly و PersianCalendar استفاده می‌کنیم (بهترین روش در .NET 6+)
                return new DateOnly(year, month, day, Pc);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"تاریخ شمسی نامعتبر است: {persianDate}", ex);
            }
        }

        public static string ToPersianDate(this DateOnly date)
        {
            var pc = new PersianCalendar();
            var dt = date.ToDateTime(TimeOnly.MinValue);

            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);

            return $"{year:0000}/{month:00}/{day:00}";
        }
    }
}