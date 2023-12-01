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


    }
}
