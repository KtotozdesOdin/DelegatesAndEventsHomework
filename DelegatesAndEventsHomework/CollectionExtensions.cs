namespace DelegatesAndEventsHomework
{
    public static class CollectionExtensions
    {
        public static T GetMax<T>(
           this IEnumerable<T> collection,
           Func<T, float> convertToNumber) where T : class
        {
            using var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException("Коллекция пуста.");

            var maxElement = enumerator.Current;
            var maxNumber = convertToNumber(maxElement);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var number = convertToNumber(current);
                if (number > maxNumber)
                {
                    maxNumber = number;
                    maxElement = current;
                }
            }
            return maxElement;
        }
    }
}
