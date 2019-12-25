using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 이 소스는 LGPL(GNU Lesser General Public Licence)를 따릅니다.
/// 이 컨트롤을 사용하는 데에는 제한이 없으나, 해당 소스를 변경하거나 개선했을 경우에는 LGPL에 의해 소스를 제공해야 합니다.
/// 이 소스는 누구나 자유롭게 사용할 수 있고, 배포할 수 있습니다.
/// GitHub : https://github.com/linuxwan/WANI-Grid
/// Blog : https://uniworks.tistory.com
/// 작성자 : Park Chungwan
/// </summary>
namespace WANI_Grid.Grid.Element
{
    public class Row : ICloneable
    {
        #region 변수
        private string rowKey = String.Empty;
        private Color backColor;
        private Color foreColor;
        private bool selected;
        private int maxLines;
        private bool hidden = false;
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
        public string RowKey
        {
            get { return rowKey; }
            set { rowKey = value; }
        }

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

        /// <summary>
        /// 행을 숨기거나 할 경우
        /// </summary>
        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
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
