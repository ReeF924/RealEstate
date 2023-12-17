using RealEstate.Models.DatabaseModels;
using System.Runtime.CompilerServices;

namespace RealEstate
{
    public static class Extensions
    {
        public static void ForEachExt<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }
        public static Dictionary<char, int> GetCategoryCount(this List<Offer> offers)
        {
            Dictionary<char, int> counts = new();
            counts['f'] = 0;
            counts['l'] = 0;
            counts['h'] = 0;
            counts['c'] = 0;
            offers.ForEach(offer =>
            {
                switch (offer.Category)
                {
                    case 'f':
                        counts['f']++;
                        break;
                    case 'l':
                        counts['l']++;
                        break;
                    case 'h':
                        counts['h']++;
                        break;
                    case 'c':
                        counts['c']++;
                        break;
                }
            });
            return counts;
        }
        public static string GetMessageDateString(this DateTime sentDate)
        {
            DateTime now = DateTime.UtcNow;

            sentDate = sentDate.ToLocalTime();

            if (sentDate.Date == now.Date)
            {
                return sentDate.ToString("HH:mm");
            }
            else if (sentDate.Date == now.AddDays(-1).Date)
            {
                return "YDA " + sentDate.ToString("HH:mm");
            }
            else if (sentDate.Year == now.Year)
            {
                return sentDate.ToString("dd. MM. HH:mm");
            }
            else
            {
                return sentDate.ToString("dd. MM. yyyy");
            }
        }

        public static bool CompareDateTimes(this DateTime first, DateTime second)
        {
            return DateTime.Compare(first, second) > 0;
        }
    }
}
