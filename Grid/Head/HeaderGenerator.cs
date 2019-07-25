using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WANI_Grid.Grid.Head
{
    public abstract class HeaderGenerator
    {
        #region 변수
        protected List<Header> _headers = new List<Header>();
        protected int topHeaderHeight = 20;   //Grid의 Header 높이
        protected int leftHeaderWidth = 22; //Grid의 맨 왼쪽 빈Column Width
        protected bool isLargeLastCol = false;    //마지막 컬럼의 폭이 Client 폭 보다 클 경우 true
        protected Font headerFont = new Font("맑은 고딕", 9);
        protected SolidBrush blackBrush = new SolidBrush(Color.Black);
        #endregion 변수

        #region Property
        public int TopHeaderHeight
        {
            get { return topHeaderHeight; }
            set { topHeaderHeight = value; }
        }

        public int LeftHeaderWidth
        {
            get { return leftHeaderWidth; }
            set { leftHeaderWidth = value; }
        }

        public Font HeaderFont
        {
            get { return headerFont; }
            set { headerFont = value; }
        }

        public SolidBrush BlackBrush
        {
            get { return blackBrush; }
            set { blackBrush = value; }
        }

        public bool IsLargeLastCol
        {
            get { return isLargeLastCol; }
            set { isLargeLastCol = value; }
        }
        #endregion Property

        public HeaderGenerator() { }

        public abstract void AddHeaders(object obj);
        public abstract List<Header> GetHeaders();
        public abstract void HeaderClear();
        public abstract void DrawHeaders(int firstVisibleCol, int lastVisibleCol, int controlWidth, Graphics graphics, Rectangle rect);
    }
}
