using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KnightTour
{
    class SortablePoint : IComparable
    {
        int[,] accessibility;
        public int X { get; set; }
        public int Y { get; set; }
        public SortablePoint(int x, int y, int[,] inputAccessibility)
        {
            X = x;
            Y = y;
            this.accessibility = inputAccessibility;
        }
        public int CompareTo(object obj)
        {
            if (this.accessibility[X, Y] < ((SortablePoint)obj).accessibility[X, Y])
            {
                return -1;
            }
            else if (this.accessibility[X, Y] > ((SortablePoint)obj).accessibility[X, Y])
            {
                return 1;
            }
            else // equals
            {
                return 0;
            }
        }
    }
}
