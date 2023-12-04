using RealEstate.Models.DatabaseModels;
using System.Runtime.CompilerServices;

namespace RealEstate
{
    public static class Extensions
    {
        public static void ForEachExt<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach(T item in collection)
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
                    case  'l':
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

    }
}
