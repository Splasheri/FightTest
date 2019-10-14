namespace UnityGridSystem
{
#pragma warning disable CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
    public class Skills
#pragma warning restore CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
    {
        public int Medicine { get { return _medicine; } }
        public int Strength { get { return _strength; } }
        protected int _medicine;
        protected int _strength;
        public Skills(int med = 0, int str = 0)
        {
            _medicine = med;
            _strength = str;
        }
        public void Improve(int med = 0, int str = 0)
        {
            _medicine += med;
            _strength += str;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Skills))
            {
                var skills = (Skills)obj;
                if (Medicine >= skills.Medicine && Strength >= skills.Strength)
                {
                    return true;
                }
            }
            return false;
        }
    }
}