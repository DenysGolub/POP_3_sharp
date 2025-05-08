namespace Lab3
{
    public class Program
    {
        public static List<int> GenerateRandomItemsCount(int maxItemCount)
        {
            List<int> items = new List<int>();
            Random rand = new Random();

            while (maxItemCount > 0)
            {
                int itemCount = rand.Next(1, maxItemCount + 1);
                items.Add(itemCount);
                maxItemCount -= itemCount;
            }

            return items;
        }

        public static void RunFixed(Storage storage)
        {
            int[] consumers = { 4, 4, 8, 10, 2, 2 }; // => 30
            int[] producers = { 5, 10, 15 };         // => 30

            Console.WriteLine($"Producers items count: [{string.Join(", ", producers)}]");
            Console.WriteLine($"Consumers items count: [{string.Join(", ", consumers)}]");

            for (int i = 0; i < producers.Length; i++)
            {
                new Producer(i, storage, producers[i]);
            }

            for (int i = 0; i < consumers.Length; i++)
            {
                new Consumer(i, storage, consumers[i]);
            }
        }

        public static void RunRandom(Storage storage, int maxItemCount)
        {
            List<int> producers = GenerateRandomItemsCount(maxItemCount);
            List<int> consumers = GenerateRandomItemsCount(maxItemCount);

            Console.WriteLine($"Producers items count: [{string.Join(", ", producers)}]");
            Console.WriteLine($"Consumers items count: [{string.Join(", ", consumers)}]");

            for (int i = 0; i < producers.Count; i++)
            {
                new Producer(i, storage, producers[i]);
            }

            for (int i = 0; i < consumers.Count; i++)
            {
                new Consumer(i, storage, consumers[i]);
            }
        }

        public static void Main(string[] args)
        {
            int maxStorageSize = 10;
            int maxItemCount = 50;
            bool fixedMode = false;

            Storage storage = new Storage(maxStorageSize);

            if (!fixedMode)
            {
                RunRandom(storage, maxItemCount);
            }
            else
            {
                RunFixed(storage);
            }
        }
    }

}
