using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityGridSystem
{
    public delegate void BattleAction(Character guest);
    public class BAction
    {
        private BattleAction action;
        public BAction()
        {
            action = (Character g) => { };
        }
        public BAction AddItemTest(Item item, string pass, string fail)
        {
            action += (Character guest) =>
            {
                if (guest.Have(item))
                {
                    guest.Say(pass);
                }
                else
                {
                    guest.Say(fail);
                }
            };
            return this;
        }
        public BAction AddSkillTest(Skills skill, string pass, string fail)
        {
            action += (Character guest) =>
            {
                if (guest.Can(skill))
                {
                    guest.Say(pass);
                }
                else
                {
                    guest.Say(fail);
                }
            };
            return this;
        }
        public BAction AddAnotherAction(Action<Character> anotherAction)
        {
            action += (Character ch) => { anotherAction(ch); };
            return this;
        }
        public BAction RemoveActions()
        {
            action = null;
            return this;
        }
        public void Invoke(Character character)
        {
            action?.Invoke(character);
        }
    }
}
