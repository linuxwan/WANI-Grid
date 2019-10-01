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
    public static class DrawTextUtil
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

        /// <summary>
        /// Grid Calendar Day Header를 그린다.
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
        public static void DrawGridHeaderCalendarRectangleAndText(Graphics graphics, SolidBrush brush, SolidBrush blackBrush, Pen pen, List<Header> headers, Font headerFont, int index, int columnStartX, int headerWidth)
        {
            //일자 출력
            if (headerWidth == 0) return;
            int dayYLocation = 32;
            int dayHeight = 28;

            //헤더영역의 사각형을 채우고 테두리를 그린다.
            graphics.FillRectangle(brush, columnStartX + 1, dayYLocation + 1, headerWidth, dayHeight);
            graphics.DrawRectangle(pen, columnStartX + 1, dayYLocation + 1, headerWidth, dayHeight);

            //헤더 타이틀 정렬 방법 설정
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringUtil.GetStringAlignment(sf, HorizontalAlignment.Center);            

            //헤더 타이틀을 그린다.
            Rectangle colRec = new Rectangle(columnStartX + 1, dayYLocation + 1, headerWidth, dayHeight);
            Rectangle colRec1 = new Rectangle(columnStartX + 1, dayYLocation + 1, headerWidth, dayHeight / 2);
            Rectangle colRec2 = new Rectangle(columnStartX + 1, dayYLocation + (dayHeight / 2) + 1, headerWidth, dayHeight / 2);
            if (headers == null)
            {
                graphics.DrawString("", headerFont, blackBrush, colRec, sf);
            }
            else
            {                
                if (headers[index].Width >= 5 && headers[index].Width < 15)
                {
                    Font dayFont = CalculateFontSize(graphics, headers[index].Title.Substring(0, 1), headerFont, columnStartX, dayYLocation, headerWidth, dayHeight);
                    if (dayFont.Size > 8.3) dayFont = new Font(headerFont.Name, 8.4f);
                    graphics.DrawString(headers[index].Title.Substring(0,1), dayFont, blackBrush, colRec1, sf);
                    graphics.DrawString(headers[index].Title.Substring(1,1), dayFont, blackBrush, colRec2, sf);
                } else
                {
                    Font calHeadFont = CalculateFontSize(graphics, headers[index].Title, headerFont, columnStartX, dayYLocation, headerWidth, dayHeight);
                    if (calHeadFont.Size > 8.3) calHeadFont = new Font(headerFont.Name, 8.4f);
                    graphics.DrawString(headers[index].Title, calHeadFont, blackBrush, colRec, sf);                    
                }
            }                        
        }

        /// <summary>
        /// Grid Calendar Week Number Header를 그린다.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="brush"></param>
        /// <param name="blackBrush"></param>
        /// <param name="pen"></param>
        /// <param name="headerFont"></param>
        /// <param name="dicWeekNo"></param>
        /// <param name="columnStartX"></param>
        public static void DrawGridHeaderCalendarWeekNumberRectangleAndText(Graphics graphics, SolidBrush brush, SolidBrush blackBrush, Pen pen, Font headerFont, Dictionary<int, List<int>> dicWeekNo, int columnStartX)
        {
            int dayYLocation = 16;
            int weekNoHeight = 16;
            foreach (KeyValuePair<int, List<int>> items in dicWeekNo)
            {
                int weekWidth = items.Value.Sum();
                //헤더영역의 사각형을 채우고 테두리를 그린다.
                graphics.FillRectangle(brush, columnStartX + 1, dayYLocation + 1, weekWidth, weekNoHeight);
                graphics.DrawRectangle(pen, columnStartX + 1, dayYLocation + 1, weekWidth, weekNoHeight);

                //헤더 타이틀 정렬 방법 설정
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringUtil.GetStringAlignment(sf, HorizontalAlignment.Center);

                //헤더 타이틀을 그린다.
                Rectangle colRec = new Rectangle(columnStartX + 1, dayYLocation + 1, weekWidth, weekNoHeight);

                Font calHeadFont = CalculateFontSize(graphics, items.Key.ToString(), headerFont, columnStartX, dayYLocation, weekWidth, weekNoHeight);
                graphics.DrawString(items.Key.ToString(), calHeadFont, blackBrush, colRec, sf);

                columnStartX += weekWidth;
            }                        
        }

        /// <summary>
        /// Grid Calendar Year Month Header를 그린다.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="brush"></param>
        /// <param name="blackBrush"></param>
        /// <param name="pen"></param>
        /// <param name="headerFont"></param>
        /// <param name="dicYearMonth"></param>
        /// <param name="columnStartX"></param>
        public static void DrawGridHeaderCalendarYearMonthRectangleAndText(Graphics graphics, SolidBrush brush, SolidBrush blackBrush, Pen pen, Font headerFont, Dictionary<string, List<int>> dicYearMonth, int columnStartX)
        {
            int dayYLocation = 0;
            int weekNoHeight = 16;
            foreach (KeyValuePair<string, List<int>> items in dicYearMonth)
            {
                int yearMonthWidth = items.Value.Sum();
                //헤더영역의 사각형을 채우고 테두리를 그린다.
                graphics.FillRectangle(brush, columnStartX + 1, dayYLocation + 1, yearMonthWidth, weekNoHeight);
                graphics.DrawRectangle(pen, columnStartX + 1, dayYLocation + 1, yearMonthWidth, weekNoHeight);

                //헤더 타이틀 정렬 방법 설정
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringUtil.GetStringAlignment(sf, HorizontalAlignment.Center);

                //헤더 타이틀을 그린다.
                Rectangle colRec = new Rectangle(columnStartX + 1, dayYLocation + 1, yearMonthWidth, weekNoHeight);

                Font calHeadFont = CalculateFontSize(graphics, items.Key.ToString(), headerFont, columnStartX, dayYLocation, yearMonthWidth, weekNoHeight);
                graphics.DrawString(items.Key.ToString(), calHeadFont, blackBrush, colRec, sf);                

                columnStartX += yearMonthWidth;
            }
        }

        /// <summary>
        /// 사각형 사이즈에 맞는 폰트 크기를 계산한다.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="header"></param>
        /// <param name="headerFont"></param>
        /// <param name="columnStartX"></param>
        /// <param name="columnStartY"></param>
        /// <param name="headerWidth"></param>
        /// <param name="headerHeight"></param>
        /// <returns></returns>
        public static Font CalculateFontSize(Graphics g, string header, Font headerFont, int columnStartX, int columnStartY, int headerWidth, int headerHeight)
        {
            Single Factor, FactorX, FactorY;
            Rectangle rec = new Rectangle(columnStartX + 1, columnStartY + 1, headerWidth, headerHeight);
            SizeF sz = g.MeasureString(header, headerFont);

            FactorX = headerWidth / sz.Width;
            FactorY = headerHeight / sz.Height;

            if (FactorX > FactorY) Factor = FactorY;
            else Factor = FactorX;

            if (Factor > 0)
                return new Font(headerFont.Name, headerFont.SizeInPoints * Factor);
            else
                return new Font(headerFont.Name, headerFont.SizeInPoints * 0.1f);
        }        
    }
}
