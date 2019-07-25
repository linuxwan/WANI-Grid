using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WANI_Grid.Util;
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
    public class DefaultHeaderGenerator : HeaderGenerator
    {        
        /// <summary>
        /// 컬럼 헤더 정보를 설정
        /// </summary>
        /// <param name="obj"></param>
        public override void AddHeaders(object obj)
        {
            this._headers.Add(obj as Header);            
        }

        /// <summary>
        /// 컬럼 헤더 정보를 반환
        /// </summary>
        /// <returns></returns>
        public override List<Header> GetHeaders()
        {
            return this._headers;
        }

        /// <summary>
        /// 헤더 정보를 Clear한다.
        /// </summary>
        public override void HeaderClear()
        {
            this._headers.Clear();
        }

        /// <summary>
        /// Header 그리기
        /// </summary>
        /// <param name="firstVisibleCol"></param>
        /// <param name="lastVisibleCol"></param>
        /// <param name="controlWidth"></param>
        /// <param name="graphics"></param>
        /// <param name="rect"></param>
        public override void DrawHeaders(int firstVisibleCol, int lastVisibleCol, int controlWidth, Graphics graphics, Rectangle rect)
        {
            SolidBrush brush = new SolidBrush(SystemColors.ControlLight);
            Pen pen = new Pen(Color.LightGray);

            int columnStartX = 0;
            graphics.FillRectangle(brush, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);
            graphics.DrawRectangle(pen, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);
            
            for (int i = firstVisibleCol; i <= lastVisibleCol; i++)
            {
                if (i == firstVisibleCol) columnStartX += leftHeaderWidth;  //첫 시작컬럼의 폭을 leftHeaderWidth 만큼 설정
                int headerWidth = this._headers[i].Width;   //i 번째 컬럼의 폭을 설정
                
                //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                if (columnStartX  + headerWidth > controlWidth)
                {
                    headerWidth = controlWidth - columnStartX - 3;
                    if (lastVisibleCol == _headers.Count - 1) IsLargeLastCol = true;
                } else
                {
                    IsLargeLastCol = false;
                }

                //헤더영역의 사각형을 채우고 테두리를 그린다.
                graphics.FillRectangle(brush, columnStartX + 1, 1, headerWidth, topHeaderHeight);
                graphics.DrawRectangle(pen, columnStartX + 1, 1, headerWidth, topHeaderHeight);

                //헤더 타이틀 정렬 방법 설정
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringUtil.GetStringAlignment(sf, HorizontalAlignment.Center);

                //헤더 타이틀을 그린다.
                Rectangle colRec = new Rectangle(columnStartX + 1, 1, headerWidth, topHeaderHeight);
                graphics.DrawString(this._headers[i].Title, headerFont, blackBrush, colRec, sf);

                columnStartX += headerWidth;
            }
        }
    }
}
