using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WANI_Grid.Grid.Head;
/// <summary>
/// 이 소스는 LGPL(GNU Lesser General Public Licence)를 따릅니다.
/// 이 컨트롤을 사용하는 데에는 제한이 없으나, 해당 소스를 변경하거나 개선했을 경우에는 LGPL에 의해 소스를 제공해야 합니다.
/// 이 소스는 누구나 자유롭게 사용할 수 있고, 배포할 수 있습니다.
/// GitHub : https://github.com/linuxwan/WANI-Grid
/// Blog : https://uniworks.tistory.com
/// 작성자 : Park Chungwan
/// </summary>
namespace WANI_Grid.Grid
{
    public class Grid
    {
        #region 변수
        private HeaderGenerator headerGen;
        private int topHeaderHeight = 20;   //Grid의 Header 높이
        private int leftHeaderWidth = 22;   //Grid의 맨 왼쪽 빈Column Width
        private int firstVisibleCol = 0;    //화면상에서 처음 보여져야할 컬럼
        private int lastVisibleCol = 0;     //화면상에서 마지막에보여질 컬럼    
        private int firstVisibleRow = 0;    //화면상에서 처음 보여질 로우
        private int lastVisibleRow = 0;     //화면상에서 마지막에보여질 로우                
        #endregion 변수

        #region Property
        /// <summary>
        /// Grid 맨 왼쪽 Column의 폭
        /// </summary>
        public int LeftHeaderWidth
        {
            get { return leftHeaderWidth; }
            set { leftHeaderWidth = value; }
        }

        /// <summary>
        /// Grid Header 영역의 높이
        /// </summary>
        public int TopHeaderHeight
        {
            get { return topHeaderHeight; }
            set { topHeaderHeight = value; }
        }

        /// <summary>
        /// Grid Column별 Header 정보를 List형태로 관리
        /// </summary>
        public List<Header> GridHeaderList
        {
            get
            {
                if (headerGen == null) return null;
                return headerGen.GetHeaders();
            }
        }

        private GridType gridDisplayType = GridType.DefaultType;
        public GridType GridDisplayType
        {
            get { return gridDisplayType; }
            set { gridDisplayType = value; }
        }

        public HeaderGenerator HeaderGen
        {
            get { return headerGen; }
            set { headerGen = value; }
        }

        public int FirstVisibleCol
        {
            get { return firstVisibleCol; }
            set { firstVisibleCol = value; }
        }

        public int LastVisibleCol
        {
            get { return lastVisibleCol; }
            set { lastVisibleCol = value; }
        }

        public int FirstVisibleRow
        {
            get { return firstVisibleRow; }
            set { firstVisibleRow = value; }
        }

        public int LastVisibleRow
        {
            get { return lastVisibleRow; }
            set { lastVisibleRow = value; }
        }
        #endregion Property

        #region Method

        public void DrawHeader(Graphics graphics, Rectangle rect, int clientWidth, int colFixed)
        {
            if (headerGen != null)
            {
                if (colFixed < 0) colFixed = 0;
                if (colFixed == 0)
                {
                    headerGen.DrawHeaders(firstVisibleCol, lastVisibleCol, clientWidth, graphics, rect);
                }
                else
                {
                    headerGen.DrawHeaders(colFixed, firstVisibleCol, lastVisibleCol, clientWidth, graphics, rect);
                }
            }
        }

        #endregion Method
    }
}
