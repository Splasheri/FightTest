using System.Collections.Generic;
using UnityEngine;

namespace UnityGridSystem
{
    public class EventWatcher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Стиль", "IDE0044:Добавить модификатор только для чтения", Justification = "<Ожидание>")]
        private List<string> log;
        public EventWatcher()
        {
            log = new List<string>();
        }
        public void AddString(string s)
        {
            log.Add(s);
            Debug.Log(s);
        }        
    }
}