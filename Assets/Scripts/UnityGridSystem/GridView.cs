using System.Collections.Generic;
using UnityEngine;

namespace UnityGridSystem
{
    public class GridView : MonoBehaviour
    {
        private List<CellView> _viewCells;
        public CellView cellView;
        public BattleGrid grid;
        public void CreateView()
        {
            _viewCells = new List<CellView>();
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Cols; j++)
                {
                    _viewCells.Add(UnityEngine.Object.Instantiate(cellView, new Vector3(j*2.5f, i*-2.5f), Quaternion.Euler(0, 0, 0), transform).SetModel(grid[(i * grid.Cols) + j]));
                }
            }
        }
    }
}
