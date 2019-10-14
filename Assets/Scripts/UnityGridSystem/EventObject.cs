using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGridSystem
{
    public class EventObject : BSObject
    {
        public EventObject(EventWatcher watcher, BAction action) : base(watcher, action)
        {
        }
    }
}
