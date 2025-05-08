namespace Lab3
{
    public class Producer
    {
        private int itemNumbersToProduce;
        private Storage storage;
        private int id;

        public Producer(int id, Storage storage, int itemNumbersToProduce)
        {
            this.itemNumbersToProduce = itemNumbersToProduce;
            this.storage = storage;
            this.id = id;

            Thread thread = new Thread(Run);
            thread.Start();
        }

        private void Run()
        {
            for (int i = 0; i < itemNumbersToProduce; i++)
            {
                storage.ProduceItem(id, i);
            }
            Console.WriteLine($"Producer {id} ended his work.");
        }
    }

}
