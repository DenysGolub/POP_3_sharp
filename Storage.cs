namespace Lab3
{
    public class Storage
    {
        private Semaphore storageAccess; // Only one user at a time
        private Semaphore emptyStorage;  // Notifying consumers
        private Semaphore fullStorage;   // Notifying producers

        private List<Item> items;

        public Storage(int size)
        {
            items = new List<Item>(size);

            storageAccess = new Semaphore(1, 1);
            fullStorage = new Semaphore(size, size);
            emptyStorage = new Semaphore(0, size);
        }

        public void ProduceItem(int producerId, int itemId)
        {
            Console.WriteLine($"Producer {producerId} near the Storage.");
            Console.WriteLine($"Producer {producerId} is waiting for free space in the Storage.");

            fullStorage.WaitOne();

            Console.WriteLine($"Producer {producerId} can put item to the Storage.");
            Console.WriteLine($"Producer {producerId} is waiting for access to the Storage.");
            storageAccess.WaitOne();


            Console.WriteLine($"Producer {producerId} in the Storage.");

            Item item = new Item($"{producerId}_{itemId}");
            PutItem(item);

            Console.WriteLine($"Producer {producerId} put item {item.GetName()} to the Storage.");
            Console.WriteLine($"Producer {producerId} exited from the Storage.");

            storageAccess.Release();

            Console.WriteLine($"Producer {producerId} notifying Consumers about new Item in the Storage.");

            emptyStorage.Release();
        }

        public void ConsumeItem(int consumerId)
        {
            Console.WriteLine($"Consumer {consumerId} near the Storage.");
            Console.WriteLine($"Consumer {consumerId} is waiting for new items in the Storage.");

            emptyStorage.WaitOne();

            Console.WriteLine($"Consumer {consumerId} can get item in the Storage.");
            Console.WriteLine($"Consumer {consumerId} waiting for access to enter the Storage.");

            storageAccess.WaitOne();

            Console.WriteLine($"Consumer {consumerId} in the Storage.");
            Item item = GetItem();

            Console.WriteLine($"Consumer {consumerId} got item {item.GetName()} from the Storage.");

            storageAccess.Release();

            Console.WriteLine($"Consumer {consumerId} quited from the Storage.");

            fullStorage.Release();

            Console.WriteLine($"Consumer {consumerId} is notifying Producer about free space for items in the Storage.");
        }


        private void PutItem(Item item)
        {
            items.Add(item);
        }

        private Item GetItem()
        {
            var item = items[0];
            items.RemoveAt(0);
            return item;
        }
    }

}
