using ConsoleGridSystem;

namespace UnityGridSystem
{
    public class BattleCell : BaseCell
    {
        private BSObject _holder;
        public BSObject Holder => _holder;
        protected Character actor;
        public BattleCell(BSObject holder = null) : base()
        {
            _holder = holder;
        }
        public BattleCell MoveTo(BattleCell target)
        {
            target.Set(s.holded);
            this.Set(s.passed);
            target.actor = (Character)_holder;
            _holder = null;
            target.Act();
            OnMove();
            target.OnMove();
            return target;
        }
        public new BattleCell Set(s state)
        {
            return (BattleCell)base.Set(state);
        }
        protected void Act()
        {
            _holder?.Act(actor);
            _holder = actor;
        }
        internal BattleCell Put(BSObject obj)
        {
            _holder = obj;
            return this;
        }
    }
}
