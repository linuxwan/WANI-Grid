using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WANI_Grid.Grid.Element
{
    public class Row : ICloneable
    {
        #region 변수
        private Color backColor;
        private Color foreColor;
        private bool selected;
        private int maxLines;
        private DataRow row;
        #endregion 변수

        #region 생성자
        public Row()
        {
            maxLines = 1;
            selected = false;
            foreColor = Color.Black;
            backColor = Color.LightGray;
        }

        public Row(DataRow dRow)
        {
            maxLines = 1;
            selected = false;
            foreColor = Color.Black;
            backColor = Color.LightGray;
            row = dRow;
        }
        #endregion 생성자

        #region Properties
        public int MaxLines
        {
            get { return maxLines; }
            set
            {
                if (maxLines != value) maxLines = value;
            }
        }

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public DataRow DataRow
        {
            get { return row; }
            set { row = value; }
        }
        #endregion Properties

        #region Method
        public object Clone()
        {
            Row row = new Row();
            row.BackColor = backColor;
            row.ForeColor = foreColor;
            row.Selected = selected;
            return row;
        }
        #endregion Method
    }
}
