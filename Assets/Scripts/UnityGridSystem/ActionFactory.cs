using System;

namespace UnityGridSystem
{ 

    public class ActionFactory
    {
        private readonly EventWatcher _ew;
        public ActionFactory(EventWatcher ew) {
            _ew = ew; 
        }       
        public BAction CreateAction() {            
            return new BAction();
        }

        public EventObject CreateEvent(BAction action)
        {
            return new EventObject(_ew, action);
        }
    }
}
