using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGridSystem
{
    public class CharacterFactory
    {
        private EventWatcher _ew;
        public CharacterFactory(EventWatcher ew) 
        { 
            _ew = ew; 
        }
        public Character CreateCharacter(
            List<Item> startItems = null,
            Skills startSkills = null
            )
        {
            Character character = new Character(_ew);
            if (startItems!=null)
            {
                foreach (var item in startItems)
                {
                    character.AddItem(item);
                }
            }
            if (startSkills!=null)
            {
                character.SetSkills(startSkills);
            }
            return character;
        }
    }
}
