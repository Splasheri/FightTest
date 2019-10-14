namespace UnityGridSystem
{
#pragma warning disable CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
    public class Item
#pragma warning restore CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
    {        
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public Item(string name, int count = 0)
        {
            Name = name;
            Amount = count;
        }
        public bool RemoveSome(int count)
        {
            Amount -= count;
            return Amount == 0;
        }
        public Item Clone()
        {
            return new Item(this.Name, this.Amount);
        }
        public Item SetAmount(int value)
        {
            Amount = value;
            return this;
        }
        public override bool Equals(object obj)
        {           
            if (obj.GetType()==typeof(Item))
            {
                if (Name==null)
                {
                    return true;
                }
                var secondItem = (Item)obj;
                if (secondItem.Name == Name && secondItem.Amount <= this.Amount)
                {
                    return true;
                }
            }
            return false;
        }
    }
}