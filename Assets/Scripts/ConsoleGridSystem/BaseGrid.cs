using System.Collections.Generic;

namespace ConsoleGridSystem
{
    public class BaseGrid
    {
        public List<BaseCell> _cells;
        private readonly int _rows;
        private readonly int _cols;

        public int Cols => _cols;
        public int Rows => _rows;

        public BaseGrid(int cols, int rows)
        {
            _cols = cols;
            _rows = rows;
            Initialize();
        }
        protected virtual void Initialize()
        {
            _cells = new List<BaseCell>();
            for (int i = 0; i < _cols * _rows; i++)
            {
                _cells.Add(new BaseCell());
            }
        }
        public BaseCell this[int index] => _cells[index];
        public int IndexOf(BaseCell cell)
        {
            return _cells.IndexOf(cell);
        }
        public void SetCells(int[] positions, s state)
        {
            foreach (int index in positions)
            {
                _cells[index].Set(state);
            }
        }
        public void SetCells(int[] positions, BaseCell[] cells)
        {
            if (positions.Length!=cells.Length)
            {
                throw new System.IndexOutOfRangeException();
            }
            for (int i = 0; i < positions.Length; i++)
            {
                _cells[i] = cells[i];
            }
        }
        public BaseCell RandomNeighbour(BaseCell startCell)
        {
            BaseCell cell = Neighbours(startCell).Filter().Random();
            return cell ?? startCell;
        }
        public int RandomNeighbour(int startCell)
        {
            BaseCell cell = Neighbours(_cells[startCell]).Filter().Random();
            return cell == null ? startCell : _cells.IndexOf(cell);
        }
        public void ConsoleDisplay()
        {
            for (int j = 0; j < _rows; j++)
            {
                for (int i = 0; i < _cols; i++)
                {
                    var index = j * _cols + i;
                    System.Console.Write((int)_cells[index].State + "  ");
                }
                System.Console.Write("\n");
            }
        }
        private List<BaseCell> Neighbours(BaseCell cell)
        {
            int startPosition = _cells.IndexOf(cell);
            List<BaseCell> neighbours = new List<BaseCell>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighPosition = startPosition + i * _cols + j;
                    if (ValidatePosition(startPosition, neighPosition))
                    {
                        neighbours.Add(_cells[neighPosition]);
                    }
                }
            }
            return neighbours;
        }
        private bool ValidatePosition(int startIndex, int neighIndex)
        {
            return (neighIndex != startIndex) &&
                   (neighIndex >= 0) &&
                   (neighIndex < _cols * _rows) &&
                   (System.Math.Abs(startIndex % _cols - neighIndex % _cols) <= 1) &&
                   (System.Math.Abs(startIndex / _cols - neighIndex / _cols) <= 1);
        }
    }
}
