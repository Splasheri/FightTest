using System;

namespace UnityGridSystem
{
    public abstract class BSObject
    {
        protected readonly EventWatcher _watcher;
        protected BAction _action;
        protected BSObject(EventWatcher watcher, BAction action = null)
        {
            _action = action;
            _watcher = watcher;
        }
        public void Act(Character guest)
        {
            _action?.Invoke(guest);
            _action = null;
        }
    }
}
