using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGridSystem
{
    public static class IListExtensions
    {
        public static BaseCell Random(this IList<BaseCell> cells)
        {
            if (cells.Count==0)
            {
                return null;
            }
            return cells[UnityEngine.Random.Range(0, cells.Count)];
        }
        public static BaseCell RandomEmpty(this IList<BaseCell> cells)
        {
            var list = cells.FilterOnlyEmpty();
            if (list.Count==0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return list.Random();
        }
        public static List<BaseCell> Filter(this IList<BaseCell> cells)
        {
            List<BaseCell> firstQueue = new List<BaseCell>();
            List<BaseCell> secondQueue = new List<BaseCell>();
            for (int i = 0; i < cells.Count; i++)
            {
                switch (cells[i].State)
                {
                    case s.passed:
                        secondQueue.Add(cells[i]);
                        break;
                    case s.empty:
                        firstQueue.Add(cells[i]);
                        break;
                    case s.action:
                        return new List<BaseCell>() { cells[i] };
                }
            }
            return firstQueue.Count == 0 ? secondQueue : firstQueue;
        }
        public static List<BaseCell> FilterOnlyEmpty(this IList<BaseCell> cells)
        {
            List<BaseCell> firstQueue = new List<BaseCell>();
            foreach (var cell in cells)
            {
                if (cell.Is(s.empty)) { firstQueue.Add(cell); } 
            }            
            return firstQueue;
        }
    }
}
