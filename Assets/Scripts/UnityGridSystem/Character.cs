using System.Collections.Generic;

namespace UnityGridSystem
{
    public class Character : BSObject
    {
        protected List<Item> inventory;
        protected Skills skills;
        public Skills Skill { get => skills; }
        public Character(EventWatcher watcher, BAction action = null) : base(watcher, action) 
        {
            inventory = new List<Item>();
            skills = new Skills();
        }     
        public bool Have(Item item)
        {
            if (item == null)
            {
                return true;
            }
            return inventory.Use(item) ?
                true : false ;
        }
        public bool Can(Skills check)
        {
            if (check==null)
            {
                return true;
            }
            return skills.Equals(check);
        }
        public void Say(string message)
        {
            _watcher.AddString(message);
        }
        public void AddItem(Item item)
        {
            inventory.Add(item);
        }
        public void SetSkills(Skills s)
        {
            Skill.Improve(s.Medicine,s.Strength);
        }
    }
}
