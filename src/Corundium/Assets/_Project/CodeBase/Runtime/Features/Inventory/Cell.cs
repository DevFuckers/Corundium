namespace CodeBase.Inventory
{
    public class Cell
    {
        public string Id { get; private set; }

        public int Amount { get; private set; }
        //public int Capacity;

        public Cell(string id, int amount)
        {
            Id = id;
            Amount = amount;
        }

        public override string ToString() =>
            $"Id: {Id}, Amount: {Amount}";

        public void Add(int amount)
        {
            if (amount > 0)
                Amount += amount;
        }
    }
}