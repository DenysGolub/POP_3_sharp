namespace Lab3
{
    public class Consumer
    {
        private int itemNumbersToConsume;
        private Storage storage;
        private int id;

        public Consumer(int id, Storage storage, int itemNumbersToConsume)
        {
            this.itemNumbersToConsume = itemNumbersToConsume;
            this.storage = storage;
            this.id = id;

            Thread thread = new Thread(Run);
            thread.Start();
        }

        private void Run()
        {
            for (int i = 0; i < itemNumbersToConsume; i++)
            {
                storage.ConsumeItem(id);
            }
            Console.WriteLine($"Consumer {id} ended his work.");
        }
    }

}
