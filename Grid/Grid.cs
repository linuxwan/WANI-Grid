using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WANI_Grid.Grid.Head;

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

        public void DrawHeader(Graphics graphics, Rectangle rect, int clientWidth)
        {
            if (headerGen != null)
            {
                headerGen.DrawHeaders(firstVisibleCol, lastVisibleCol, clientWidth, graphics, rect);
            }
        }

        #endregion Method
    }
}
