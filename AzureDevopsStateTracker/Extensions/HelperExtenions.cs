using System;
using System.Linq;

namespace AzureDevopsStateTracker.Extensions
{
    public static class HelperExtenions
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text?.Trim());
        }

        public static string ExtractEmail(this string user)
        {
            if (user is null)
                return user;

            if (!user.Contains(" <") && !user.TrimEnd().Contains(">"))
                return user;

            return user.Split("<").LastOrDefault().Split(">").FirstOrDefault();
        }

        public static string ToTextTime(this TimeSpan timeSpan)
        {
            if (timeSpan.Days > 0)
                return $@"{timeSpan:%d} Dia(s) {timeSpan:%h} h e {timeSpan:%m} min e {timeSpan:%s} s";

            if (timeSpan.Hours > 0)
                return $@"{timeSpan:%h} h e {timeSpan:%m} min e {timeSpan:%s} s";

            if (timeSpan.Minutes > 0)
                return $@"{timeSpan:%m} min e {timeSpan:%s} s";

            if (timeSpan.Seconds > 0)
                return $@"{timeSpan:%s} s";

            return "-";
        }
    }
}