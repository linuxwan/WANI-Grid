using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WANI_Grid.Grid.Element;
using WANI_Grid.Grid.Head;
/// <summary>
/// 이 소스는 LGPL(GNU Lesser General Public Licence)를 따릅니다.
/// 이 컨트롤을 사용하는 데에는 제한이 없으나, 해당 소스를 변경하거나 개선했을 경우에는 LGPL에 의해 소스를 제공해야 합니다.
/// 이 소스는 누구나 자유롭게 사용할 수 있고, 배포할 수 있습니다.
/// GitHub : https://github.com/linuxwan/WANI-Grid
/// Blog : https://uniworks.tistory.com
/// 작성자 : Park Chungwan
/// </summary>
namespace WANI_Grid.Util
{
    public static class DrawUtil
    {
        /// <summary>
        /// Grid Header를 그린다.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="brush"></param>
        /// <param name="blackBrush"></param>
        /// <param name="pen"></param>
        /// <param name="headers"></param>
        /// <param name="headerFont"></param>
        /// <param name="index"></param>
        /// <param name="columnStartX"></param>
        /// <param name="headerWidth"></param>
        /// <param name="topHeaderHeight"></param>
        public static void DrawGridHeaderRectangleAndText(Graphics graphics, SolidBrush brush, SolidBrush blackBrush, Pen pen, List<Header> headers, Font headerFont, int index, int columnStartX, int headerWidth, int topHeaderHeight)
        {
            if (headerWidth == 0) return;
            //헤더영역의 사각형을 채우고 테두리를 그린다.
            graphics.FillRectangle(brush, columnStartX + 1, 1, headerWidth, topHeaderHeight);
            graphics.DrawRectangle(pen, columnStartX + 1, 1, headerWidth, topHeaderHeight);

            //헤더 타이틀 정렬 방법 설정
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringUtil.GetStringAlignment(sf, HorizontalAlignment.Center);

            //헤더 타이틀을 그린다.
            Rectangle colRec = new Rectangle(columnStartX + 1, 1, headerWidth, topHeaderHeight);
            if (headers == null)
            {
                graphics.DrawString("", headerFont, blackBrush, colRec, sf);
            }
            else
            {
                graphics.DrawString(headers[index].Title, headerFont, blackBrush, colRec, sf);
            }
        }
    }
}
