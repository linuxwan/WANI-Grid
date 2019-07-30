using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WANI_Grid.Grid.Element
{
    public class Cell
    {
        private int row;
        private int col;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        public Cell(int r, int c)
        {
            row = r;
            col = c;
        }

        public void Clear()
        {
            row = -1;
            col = -1;
        }
    }
}
