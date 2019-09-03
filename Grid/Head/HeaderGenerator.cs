using System;
using System.Collections.Generic;
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
        public abstract void DrawHeaders(int colFixed, int firstVisibleCol, int lastVisibleCol, int controlWidth, Graphics graphics, Rectangle rect);

        /// <summary>
        /// FixedCol 수에 맞는 Header의 Index값을 리턴
        /// fixedCol 2이지만 실제 Header Column의 Visible 값이 false인 경우 제외하고 실제 Header Column의 Index를 구한다.
        /// </summary>
        /// <returns></returns>
        protected int GetLastFixedCol(int colFixed)
        {
            int lastFixedCol = 0;
            int startIndex = 0;
            foreach (Header head in _headers)
            {
                if (startIndex == colFixed) break;
                if (head.Visible)
                {
                    lastFixedCol = head.Index;
                    startIndex++;
                }
            }
            return lastFixedCol;
        }
    }
}
