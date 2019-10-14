using ConsoleGridSystem;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGridSystem
{
    public class CellView : MonoBehaviour
    {
        private BaseCell cell;
        public List<Sprite> sprites;
        public s State;
        public CellView SetModel(BaseCell modelCell)
        {
            cell = modelCell;
            UpdateView();
            modelCell.OnMove += UpdateView;
            return this;
        }
        public void UpdateView()
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[(int)cell.State];
            this.State = cell.State;
        }
    }
}
