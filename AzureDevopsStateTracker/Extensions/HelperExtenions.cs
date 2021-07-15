using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AzureDevopsStateTracker.Extensions
{
    public static class HelperExtenions
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text?.Trim());
        }

        /// <summary>
        /// Calculates the sum of the given timeSpans.
        /// </summary>
        public static TimeSpan Sum(this IEnumerable<TimeSpan> timeSpans)
        {
            TimeSpan sumTillNowTimeSpan = TimeSpan.Zero;

            foreach (TimeSpan timeSpan in timeSpans)
                sumTillNowTimeSpan += timeSpan;

            return sumTillNowTimeSpan;
        }

        public static string ExtractEmail(this string user)
        {
            if (user is null)
                return user;

            if (!user.Contains(" <") && !user.TrimEnd().Contains(">"))
                return user;

            return user.Split("<").LastOrDefault().Split(">").FirstOrDefault();
        }
    }
}