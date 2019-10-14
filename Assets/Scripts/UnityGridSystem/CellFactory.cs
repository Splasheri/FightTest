using ConsoleGridSystem;

namespace UnityGridSystem
{
    public class CellFactory
    {
        public CellFactory() { }
        public BattleCell CreateCell(s state, BSObject holder = null)
        {
            return new BattleCell(holder).Set(state);            
        }
    }
}
