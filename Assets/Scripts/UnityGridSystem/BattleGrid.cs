using ConsoleGridSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityGridSystem
{
    public class BattleGrid : BaseGrid
    {
        public BattleGrid(int cols, int rows) : base(cols, rows)
        {
        }
        protected override void Initialize()
        {
            _cells = new List<BaseCell>();
            for (int i = 0; i < Cols * Rows; i++)
            {
                _cells.Add(new BattleCell().Set(s.empty));
            }
        }
        public BattleCell PlaceObjectRandom(BSObject obj, s state)
        {
            return (_cells.RandomEmpty() as BattleCell).Put(obj).Set(state);
        }
        public void PlaceCellsRandom(List<BattleCell> cellsToPlace)
        {
            foreach (var cell in cellsToPlace)
            {
                _cells[_cells.IndexOf(_cells.RandomEmpty())] = cell; 
            }
        }
    }
}
