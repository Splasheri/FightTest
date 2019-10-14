using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGridSystem
{
    public delegate void MoveEvent();
#pragma warning disable IDE1006 // Стили именования
    public enum s
#pragma warning restore IDE1006 // Стили именования
    {
        holded = 0,
        passed,
        empty,
        action,
    }
    public class BaseCell
    {
        public MoveEvent OnMove;
        protected s _state;
        public s State { get => _state; }
        public int Importance { get => (int)_state; }

        public BaseCell()
        {
            OnMove = () => { };
            _state = s.empty;
        }
        public BaseCell Set(s state)
        {
            _state = state;
            return this;
        }
        public bool Is(s state)
        {
            return _state == state;
        }
        public BaseCell MoveTo(BaseCell target)
        {
            target.Set(this.State);
            this.Set(s.passed);
            return target;
        }
    }
}
