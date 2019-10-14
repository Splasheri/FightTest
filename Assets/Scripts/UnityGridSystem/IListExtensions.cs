using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityGridSystem;

namespace UnityGridSystem
{
    public static class IListExtensions
    {
        public static bool Use(this IList<Item> ilist, Item item)
        {
            var index = ilist.IndexOf(item);
            if (index>=0)
            {
                if (ilist[index].RemoveSome(item.Amount))
                {
                    ilist.RemoveAt(index);
                }
                return true;
            }
            return false;
        }
    }
}
