using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class YearMonthWeekNoDayHeaderGenerator : HeaderGenerator
    {        
        #region 생성자
        public YearMonthWeekNoDayHeaderGenerator()
        {
            TopHeaderHeight = 60;
            this._headers.Clear();
        }
        #endregion 생성자       

        public override void AddHeaders(object obj)
        {
            this._headers.Add(obj as Header);
        }

        public override List<Header> GetHeaders()
        {
            return this._headers;
        }

        public override void HeaderClear()
        {
            this._headers.Clear();
        }

        public override void DrawHeaders(int firstVisibleCol, int lastVisibleCol, int controlWidth, Graphics graphics, Rectangle rect)
        {
            SolidBrush brush = new SolidBrush(SystemColors.ControlLight);
            Pen pen = new Pen(Color.LightGray);

            int columnStartX = 0;
            int calendarStartX = 0;            

            graphics.FillRectangle(brush, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);
            graphics.DrawRectangle(pen, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);

            columnStartX += leftHeaderWidth;  //첫 시작컬럼의 폭을 leftHeaderWidth 만큼 설정 

            //주차 기간을 저장하기 위한 Dictionary - width 계산을 위해 필요
            Dictionary<int, List<int>> dicWeekNo = new Dictionary<int, List<int>>();
            //년월 기간을 저장하기 위한 Dictionary - width 계산을 위해 필요
            Dictionary<string, List<int>> dicYearMonth = new Dictionary<string, List<int>>();

            for (int i = firstVisibleCol; i <= lastVisibleCol; i++)
            {
                if (!this._headers[i].Visible) continue;
                int headerWidth = this._headers[i].Width;   //i 번째 컬럼의 폭을 설정                                    

                //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                if (columnStartX + headerWidth > controlWidth)
                {
                    headerWidth = controlWidth - columnStartX - 3;
                    if (lastVisibleCol == (this._headers.Count - 1)) IsLargeLastCol = true;
                }
                else
                {
                    IsLargeLastCol = false;
                }

                YearMonthWeekNoDayHeader yearMonthHeader = this._headers[i] as YearMonthWeekNoDayHeader;
                if (yearMonthHeader.GetDateTime <= DateTime.MinValue)
                {
                    //Grid Header를 그린다.
                    DrawTextUtil.DrawGridHeaderRectangleAndText(graphics, brush, blackBrush, pen, this._headers, headerFont, i, columnStartX, headerWidth, topHeaderHeight);
                }
                else //Calendar 날짜 형태의 컬럼을 그리기 위한 사전 준비
                {
                    if (calendarStartX == 0) calendarStartX = columnStartX;
                    //Calendar 날짜 형태의 컬럼 Header 그리기(일)
                    DrawTextUtil.DrawGridHeaderCalendarRectangleAndText(graphics, brush, blackBrush, pen, this._headers, headerFont, i, columnStartX, headerWidth);
                    if (!dicWeekNo.ContainsKey(yearMonthHeader.WeekNumber)) dicWeekNo.Add(yearMonthHeader.WeekNumber, new List<int>());
                    if (yearMonthHeader.Visible) dicWeekNo[yearMonthHeader.WeekNumber].Add(yearMonthHeader.Width);
                    if (!dicYearMonth.ContainsKey(yearMonthHeader.YearMonth)) dicYearMonth.Add(yearMonthHeader.YearMonth, new List<int>());
                    if (yearMonthHeader.Visible) dicYearMonth[yearMonthHeader.YearMonth].Add(yearMonthHeader.Width);
                }
                columnStartX += headerWidth;
            }
            //Calendar 날짜 형태의 컬럼 Header 그리기(주차/년월)
            DrawTextUtil.DrawGridHeaderCalendarWeekNumberRectangleAndText(graphics, brush, blackBrush, pen, headerFont, dicWeekNo, calendarStartX);
            DrawTextUtil.DrawGridHeaderCalendarYearMonthRectangleAndText(graphics, brush, blackBrush, pen, headerFont, dicYearMonth, calendarStartX);            
        }

        public override void DrawHeaders(int colFixed, int firstVisibleCol, int lastVisibleCol, int controlWidth, Graphics graphics, Rectangle rect, bool fixedColEditable)
        {
            SolidBrush brush = new SolidBrush(SystemColors.ControlLight);
            Pen pen = new Pen(Color.LightGray);

            int columnStartX = 0;
            int calendarStartX = 0;
            graphics.FillRectangle(brush, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);
            graphics.DrawRectangle(pen, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);

            columnStartX += leftHeaderWidth;  //첫 시작컬럼의 폭을 leftHeaderWidth 만큼 설정             
            int fixCol = this.GetLastFixedCol(colFixed);
            //주차 기간을 저장하기 위한 Dictionary - width 계산을 위해 필요
            Dictionary<int, List<int>> dicWeekNo = new Dictionary<int, List<int>>();
            //년월 기간을 저장하기 위한 Dictionary - width 계산을 위해 필요
            Dictionary<string, List<int>> dicYearMonth = new Dictionary<string, List<int>>();
            
            for (int i = 0; i <= fixCol; i++)
            {
                if (!this._headers[i].Visible) continue;
                int headerWidth = this._headers[i].Width;   //i 번째 컬럼의 폭을 설정  

                //고정컬럼 수정여부 확인
                if (!fixedColEditable) this._headers[i].Editable = false;

                //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                if (columnStartX + headerWidth > controlWidth)
                {
                    headerWidth = controlWidth - columnStartX - 3;
                    if (lastVisibleCol == (this._headers.Count - 1)) IsLargeLastCol = true;
                }
                else
                {
                    IsLargeLastCol = false;
                }

                YearMonthWeekNoDayHeader yearMonthHeader = this._headers[i] as YearMonthWeekNoDayHeader;            
                DrawTextUtil.DrawGridHeaderRectangleAndText(graphics, brush, blackBrush, pen, this._headers, headerFont, i, columnStartX, headerWidth, topHeaderHeight);
                
                columnStartX += headerWidth;                
            }            

            for (int i = firstVisibleCol + fixCol + 1; i <= lastVisibleCol && i < this._headers.Count; i++)
            {
                if (!this._headers[i].Visible) continue;
                int headerWidth = this._headers[i].Width;   //i 번째 컬럼의 폭을 설정                                    

                //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                if (columnStartX + headerWidth > controlWidth)
                {
                    headerWidth = controlWidth - columnStartX - 3;
                    if (lastVisibleCol == (this._headers.Count - 1)) IsLargeLastCol = true;
                }
                else
                {
                    IsLargeLastCol = false;
                }                

                YearMonthWeekNoDayHeader yearMonthHeader = this._headers[i] as YearMonthWeekNoDayHeader;
                if (yearMonthHeader.GetDateTime <= DateTime.MinValue)
                {
                    //Grid Header를 그린다.
                    DrawTextUtil.DrawGridHeaderRectangleAndText(graphics, brush, blackBrush, pen, this._headers, headerFont, i, columnStartX, headerWidth, topHeaderHeight);
                }
                else //Calendar 날짜 형태의 컬럼을 그리기 위한 사전 준비 및 일자 그리기
                {
                    if (calendarStartX == 0) calendarStartX = columnStartX;                    
                    if (!dicWeekNo.ContainsKey(yearMonthHeader.WeekNumber)) dicWeekNo.Add(yearMonthHeader.WeekNumber, new List<int>());
                    if (yearMonthHeader.Visible) dicWeekNo[yearMonthHeader.WeekNumber].Add(yearMonthHeader.Width);
                    if (!dicYearMonth.ContainsKey(yearMonthHeader.YearMonth)) dicYearMonth.Add(yearMonthHeader.YearMonth, new List<int>());
                    if (yearMonthHeader.Visible) dicYearMonth[yearMonthHeader.YearMonth].Add(yearMonthHeader.Width);
                    //Calendar 날짜 형태의 컬럼 Header 그리기(일)
                    DrawTextUtil.DrawGridHeaderCalendarRectangleAndText(graphics, brush, blackBrush, pen, this._headers, headerFont, i, columnStartX, headerWidth);
                }

                columnStartX += headerWidth;
            }
            //Calendar 날짜 형태의 컬럼 Header 그리기(주차/년월)
            DrawTextUtil.DrawGridHeaderCalendarWeekNumberRectangleAndText(graphics, brush, blackBrush, pen, headerFont, dicWeekNo, calendarStartX);
            DrawTextUtil.DrawGridHeaderCalendarYearMonthRectangleAndText(graphics, brush, blackBrush, pen, headerFont, dicYearMonth, calendarStartX);
        }        
    }
}
