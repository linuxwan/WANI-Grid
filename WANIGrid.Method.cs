using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WANI_Grid.Grid;
using WANI_Grid.Grid.Element;
using WANI_Grid.Grid.Head;
using WANI_Grid.Util;
/// <summary>
/// 이 소스는 LGPL(GNU Lesser General Public Licence)를 따릅니다.
/// 이 컨트롤을 사용하는 데에는 제한이 없으나, 해당 소스를 변경하거나 개선했을 경우에는 LGPL에 의해 소스를 제공해야 합니다.
/// 이 소스는 누구나 자유롭게 사용할 수 있고, 배포할 수 있습니다.
/// GitHub : https://github.com/linuxwan/WANI-Grid
/// Blog : https://uniworks.tistory.com
/// 작성자 : Park Chungwan
/// </summary>
namespace WANI_Grid
{
    public partial class WANIGrid : UserControl
    {
        #region Method
        /// <summary>
        /// 마우스 포인트에 해당하는 Grid Column의 Index를 찾아온다.
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        private int GetColFromX(int X)
        {
            int col = 0;
            int tempWidth = leftHeaderWidth;

            if (X >= 0 && X <= leftHeaderWidth)
            {
                col = -1;
            }
            else
            {
                if (colFixed == 0)
                {
                    for (col = firstVisibleCol; col < lastVisibleCol; col++)
                    {
                        if (!grid.GridHeaderList[col].Visible) continue;
                        int width = grid.GridHeaderList[col].Width;
                        if (X < width + tempWidth) break;
                        tempWidth += width;
                    }
                }
                else
                {
                    bool fixColSelected = false;
                    int fixCol = GetLastFixedCol();
                    for (col = 0; col <= fixCol && col <= lastVisibleCol; col++)
                    {
                        if (!grid.GridHeaderList[col].Visible) continue;
                        int width = grid.GridHeaderList[col].Width;
                        if (X < width + tempWidth)
                        {
                            fixColSelected = true;
                            break;
                        }
                        tempWidth += width;
                    }

                    if (!fixColSelected)
                    {
                        int fixColWidth = GetFixedColWidth();

                        for (col = firstVisibleCol + fixCol + 1; col <= lastVisibleCol; col++)
                        {
                            if (col < fixCol) continue;

                            if (!grid.GridHeaderList[col].Visible) continue;
                            int width = grid.GridHeaderList[col].Width;
                            if (X < width + tempWidth) break;
                            tempWidth += width;
                        }
                    }
                }
            }
            if (col > grid.GridHeaderList.Count - 1) col = -1;
            return col;
        }

        /// <summary>
        /// 마우스 포인트에 해당하는 Grid Row의 Index를 찾아온다.
        /// </summary>
        /// <param name="Y"></param>
        /// <returns></returns>
        private int GetRowFromY(int Y)
        {
            int row = 0;
            int tempHeight = topHeaderHeight;

            if (Y >= 0 && Y <= topHeaderHeight)
            {
                row = -1;
            }
            else
            {
                for (row = firstVisibleRow; row < lastVisibleRow; row++)
                {
                    int height = rows[row].MaxLines * rowHeight;
                    if (Y < height + tempHeight) break;
                    tempHeight += height;
                }
            }
            if (row > rows.Count - 1) row = -1;
            return row;
        }

        /// <summary>
        /// Grid 컬럼의 Header를 생성해서 추가한다.
        /// </summary>
        /// <param name="header"></param>
        public void SetHeader(HeaderBuilder headerBuilder)
        {
            if (grid == null) grid = new WANI_Grid.Grid.Grid();
            if (headerBuilder != null)
            {
                grid.HeaderGen = headerBuilder.HeaderGen;
                topHeaderHeight = headerBuilder.HeaderGen.TopHeaderHeight;
                grid.DicWeekDay = headerBuilder.DicWeekDay;
                grid.DicMonthDay = headerBuilder.DicMonthDay;
            }

            grid.GridDisplayType = headerBuilder.GridDisplayType;
            grid.TopHeaderHeight = headerBuilder.HeaderGen.TopHeaderHeight;
            InitializeScollBar();

            //dataSource가 null일 경우 Header정보를 근간으로 DataTable 생성
            if (dataSource != null)
            {
                dataSource.Clear();
                dataSource = null;
                rows.Clear();
                rowHeight = 0;
                rowsCount = 0;
                selectedCols.Clear();
                selectedRows.Clear();
                allRowsHeight = 0;                
                vScrollBar.Maximum = allRowsHeight;
                firstVisibleCol = 0;
                lastVisibleCol = 0;
                firstVisibleRow = 0;
                lastVisibleRow = 0;
            }

            if (dataSource == null)
            {
                dataSource = new DataTable();
                foreach (Header hd in grid.HeaderGen.GetHeaders())
                {
                    dataSource.Columns.Add(new DataColumn(hd.ColumnId, typeof(string)));
                }               
            }                                    
        }

        /// <summary>
        /// 가로/세로 Scrollbar 재계산
        /// </summary>
        private void ReCalcScrollBars()
        {
            if (hScrollBar == null || vScrollBar == null) return;

            if (grid != null && grid.GridHeaderList != null) allColsWidth = grid.GridHeaderList[grid.GridHeaderList.Count() - 1].Left + grid.GridHeaderList[grid.GridHeaderList.Count() - 1].Width;
            if (firstVisibleCol >= lastVisibleCol) return;
            //컬럼의 폭이 클라이언트 사이즈 폭 보다 클 경우 가로 스크롤바를 보여준다.
            if ((allColsWidth > 0) && (allColsWidth > ClientSize.Width - ysclWidth))
            {
                hScrollBar.Visible = true;
                hScrollBar.LargeChange = ((lastVisibleCol - firstVisibleCol) + 1) / 2 + 1;
                lastHScrollValue = ((lastVisibleCol - firstVisibleCol) + 1) / 2 + 1;
                hScrollBar.Maximum = grid.GridHeaderList.Count;

                //가로 스크롤바가 나타났을 때 세로 스크롤바 설정
                vScrollBar.Left = Width - vScrollBar.Width - 2;
                vScrollBar.Top = topHeaderHeight + 2;
                vScrollBar.Height = Height - topHeaderHeight - hScrollBar.Height - 4;
            }
            else
            {
                hScrollBar.Visible = false;
                grid.FirstVisibleCol = 0; //Control 크기가 바뀌면서 hScrollBar가 숨겨지면 Grid의 첫번째 컬럼 부터 그려지도록 처리
                firstVisibleCol = 0; //Control 크기가 바뀌면서 hScrollBar가 숨겨지면 Grid의 첫번째 컬럼 부터 그려지도록 처리                

                //가로 스크롤바가 숨겨졌을 때 세로 스크롤바 설정
                vScrollBar.Left = Width - vScrollBar.Width - 2;
                vScrollBar.Top = 2;
                vScrollBar.Height = Height - 4;
            }

            //로우의 높이가 클라이언트 사이즈 높이 보다 클 경우 세로 스크롤바를 보여준다.
            if (allRowsHeight > 0 && (allRowsHeight > Height - topHeaderHeight - xsclHeight))
            {
                vScrollBar.Visible = true;
                if (rowsCount == 0) rowsCount = (allRowsHeight / rowHeight) - 1;
                vScrollBar.Maximum = allRowsHeight;
                vScrollBar.LargeChange = rowHeight * 5;
                vScrollBar.SmallChange = rowHeight;

                //세로 스크롤바가 나타났을 때 가로 스크롤바 설정
                hScrollBar.Left = 1;
                hScrollBar.Width = Width - vScrollBar.Width - 2;
                hScrollBar.Top = Height - hScrollBar.Height - 2;
            }
            else
            {
                vScrollBar.Visible = false;
                grid.FirstVisibleRow = 0;

                //세로 스크롤바가 숨겨졌을 때 가로 스크롤바 설정
                hScrollBar.Left = 1;
                hScrollBar.Width = Width - 2;
                hScrollBar.Top = Height - hScrollBar.Height - 2;
            }
        }

        /// <summary>
        /// 화면에 보여져야 할 영역 계산 (화면에서 보여져야 할 첫 Column/Row, 마지막 Column/Row)
        /// </summary>
        private void CalcVisibleRange()
        {
            //Scrollbar 크기 만큼 제외해야할 영역 설정
            if (hScrollBar.Visible)
                xsclHeight = hScrollBar.Height;
            else
                xsclHeight = 4;

            if (vScrollBar.Visible)
                ysclWidth = vScrollBar.Width;
            else
                ysclWidth = 4;

            //보여져야 할 Row 영역 계산
            int tempRow = 0;
            int rowIndex = 0;
            if (firstVisibleRow < 0) firstVisibleRow = 0;
            if (rows.Count > 0)
            {
                for (rowIndex = firstVisibleRow, tempRow = 0; rowIndex < rows.Count && tempRow < Height - xsclHeight - topHeaderHeight; rowIndex++)
                {
                    tempRow += rows[rowIndex].MaxLines * rowHeight;
                }
                lastVisibleRow = rowIndex;

                if (lastVisibleRow >= rows.Count) lastVisibleRow = rows.Count - 1;

                grid.LastVisibleRow = lastVisibleRow;
            }
            else
            {
                lastVisibleRow = 0;
                grid.LastVisibleRow = lastVisibleRow;
            }

            //보여져야 할 Column 영역 계산
            if (grid.GridHeaderList != null && grid.GridHeaderList.Count > 0)
            {
                int i;
                int tempPos;
                int fixCol = GetLastFixedCol();
                if (grid.GridHeaderList.Count > 0)
                {
                    Header lastHeaderColumn = grid.GridHeaderList.Where(x => x.Visible == true).Last();

                    if (colFixed == 0)
                    {
                        for (i = firstVisibleCol, tempPos = 0; i < grid.GridHeaderList.Count && tempPos < Width - ysclWidth - leftHeaderWidth; i++)
                        {
                            if (grid.GridHeaderList[i].Visible) tempPos += grid.GridHeaderList[i].Width;
                        }
                    }
                    else
                    {
                        int fixedWidth = GetFixedColWidth();
                        for (i = firstVisibleCol + colFixed, tempPos = fixedWidth; i < grid.GridHeaderList.Count && tempPos < Width; i++)
                        {
                            if (grid.GridHeaderList[i].Visible) tempPos += grid.GridHeaderList[i].Width;
                        }
                    }

                    if (i <= grid.GridHeaderList.Count)
                    {
                        lastVisibleCol = i + 1;
                        if (lastVisibleCol >= lastHeaderColumn.Index) lastVisibleCol = lastHeaderColumn.Index;
                        grid.LastVisibleCol = lastVisibleCol;
                    } 

                    if (lastVisibleCol < 0)
                    {
                        lastVisibleCol = 0;
                        grid.LastVisibleCol = 0;
                    }
                    else if (lastVisibleCol >= lastHeaderColumn.Index)
                    {
                        lastVisibleCol = lastHeaderColumn.Index;
                        hScrollBar.Maximum = grid.GridHeaderList.Where(x => x.Visible == true).Count();
                        grid.LastVisibleCol = lastVisibleCol;
                        grid.FirstVisibleCol = firstVisibleCol;
                        currentCol = firstVisibleCol + 1;
                    } 
                }
                else
                {
                    lastVisibleCol = 0;
                    grid.LastVisibleCol = 0;
                }
            }
        }

        /// <summary>
        /// Control의 Backgroud 그리기
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rc"></param>
        private void DrawBackground(Graphics g, Rectangle rc)
        {
            g.DrawRectangle(new Pen(SystemColors.Control, 2), 0, 0, rc.Width, rc.Height);
            rc.Inflate(-1, -1);
            g.FillRectangle(new SolidBrush(SystemColors.ControlLightLight), rc);

            int lastFixedCol = GetLastFixedCol();
            if (lastFixedCol == -1)
            {
                DrawDefaultWANIGridControl(g, rc);
                return;
            }

            int enableFixCol = 0;
            for (int i = 0; i <= lastFixedCol; i++)
            {
                YearMonthWeekNoDayHeader yearMonthHeader = grid.GridHeaderList[i] as YearMonthWeekNoDayHeader;
                if (yearMonthHeader != null && yearMonthHeader.GetDateTime <= DateTime.MinValue) enableFixCol = i;
            }
            if (lastFixedCol > enableFixCol) lastFixedCol = enableFixCol;

            //선택된 컬럼의 Background를 그린다.
            SelectedColChangeBackground(g, lastFixedCol);

            //선택된 행(Row)의 Background를 그린다.
            SelectedRowChangeBackground(g, lastFixedCol);

            //휴일 색상을 그린다
            FillHolidayBackgroundColor(g);
        }

        private void FillHolidayBackgroundColor(Graphics g)
        {
            if (grid.GridDisplayType != GridType.YearMonthWeekNoDayType) return;
            //고정 컬럼이 없을 경우
            if (colFixed == 0)
            {
                int startX = leftHeaderWidth;
                for (int i = firstVisibleCol; i <= lastVisibleCol; i++)
                {
                    YearMonthWeekNoDayHeader yearMonthHeader = grid.GridHeaderList[i] as YearMonthWeekNoDayHeader;                    
                    if (yearMonthHeader.IsHoliday)
                    {                        
                        g.FillRectangle(holidayColorBrush, startX + 1, topHeaderHeight + 1, yearMonthHeader.Width, allRowsHeight - (rowHeight * firstVisibleRow) + 1);                        
                    }
                    startX += yearMonthHeader.Width;
                }
            } else
            {
                int startX = leftHeaderWidth + GetFixedColWidth();                
                for (int i = firstVisibleCol + colFixed; i <= lastVisibleCol && i < grid.GridHeaderList.Count; i++)
                {                    
                    YearMonthWeekNoDayHeader yearMonthHeader = grid.GridHeaderList[i] as YearMonthWeekNoDayHeader;                    
                    if (yearMonthHeader.IsHoliday)
                    {                        
                        g.FillRectangle(holidayColorBrush, startX + 1, topHeaderHeight + 1, yearMonthHeader.Width, allRowsHeight - (rowHeight * firstVisibleRow) + 1);                        
                    }
                    startX += grid.GridHeaderList[i].Width;
                }
            }
        }

        /// <summary>
        /// 디자인 시에 WANIGrid Control을 WinForm 위에 올렸을 때 보여지는 WANIGrid Control의 외형을 그린다.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rc"></param>
        private void DrawDefaultWANIGridControl(Graphics g, Rectangle rc)
        {
            //WANIGrid Control의 폭과 높이에 해당하는 row와 column의 갯수를 구한다. (컬럼 폭은 80으로 고정)
            int cols = (rc.Width / 80) + 1;
            int rows = (rc.Height / rowHeight) + 1;

            SolidBrush brush = new SolidBrush(SystemColors.ControlLight);
            Pen pen = new Pen(Color.LightGray);

            int columnStartX = 0;
            g.FillRectangle(brush, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);
            g.DrawRectangle(pen, columnStartX + 1, 1, leftHeaderWidth, topHeaderHeight);

            columnStartX += leftHeaderWidth;  //첫 시작컬럼의 폭을 leftHeaderWidth 만큼 설정 

            for (int i = 0; i < cols; i++)
            {                
                int headerWidth = 80;   //i 번째 컬럼의 폭을 설정                                                    

                //Grid Header를 그린다.
                DrawTextUtil.DrawGridHeaderRectangleAndText(g, brush, blackBrush, pen, null, headerFont, i, columnStartX, headerWidth, topHeaderHeight);

                columnStartX += headerWidth;
            }
            vScrollBar.Visible = true;

            for (int i = 0; i < rows; i++)
            {
                columnStartX = 0;
                g.FillRectangle(brush, columnStartX + 1, i * rowHeight, leftHeaderWidth, topHeaderHeight);
                g.DrawRectangle(pen, columnStartX + 1, i * rowHeight, leftHeaderWidth, topHeaderHeight);
                columnStartX += leftHeaderWidth;

                for (int j = 0; j < cols; j++)
                {                    
                    int colWidth = 80;
                                                
                    g.DrawRectangle(pen, columnStartX + 1, i * rowHeight, colWidth, rowHeight);
                    Rectangle rec = new Rectangle(columnStartX + 2, (i * rowHeight) + 2, columnStartX - 2, rowHeight);                        

                    columnStartX += colWidth;                    
                }
            }
            hScrollBar.Visible = true;
        }

        /// <summary>
        /// 선택된 행(Row)의 Background를 그린다.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="lastFixedCol"></param>
        private void SelectedRowChangeBackground(Graphics g, int lastFixedCol)
        {
            for (int i = 0; i < selectedRows.Count; i++)
            {
                int index = selectedRows[i];
                int top = topHeaderHeight;
                int width = 0;

                if (colFixed == 0)
                {
                    for (int k = firstVisibleCol; k <= lastVisibleCol; k++)
                    {
                        if (grid.GridHeaderList[k].Visible) width += grid.GridHeaderList[k].Width;
                    }
                }
                else
                {
                    width = GetFixedColWidth();
                    for (int k = firstVisibleCol + lastFixedCol + 1; k <= lastVisibleCol; k++)
                    {
                        if (grid.GridHeaderList[k].Visible) width += grid.GridHeaderList[k].Width;
                    }
                }

                for (int j = firstVisibleRow; j < lastVisibleRow && j < index; j++)
                {
                    top += rows[j].MaxLines * rowHeight;
                }

                if (index >= firstVisibleRow) g.FillRectangle(selectedColor, leftHeaderWidth + 1, top + 1, width, rowHeight);
            }
        }

        /// <summary>
        /// 선택된 컬럼의 Background를 그린다.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="lastFixedCol"></param>
        private void SelectedColChangeBackground(Graphics g, int lastFixedCol)
        {
            if (selectedCols == null) return;
            //선택된 컬럼의 Background를 그린다.
            for (int i = 0; i < selectedCols.Count; i++)
            {
                int index = selectedCols[i];
                int left = leftHeaderWidth;

                if (colFixed == 0)
                {
                    for (int j = firstVisibleCol; j < lastVisibleCol && j < index; j++)
                    {
                        if (!grid.GridHeaderList[j].Visible) continue;
                        left += grid.GridHeaderList[j].Width;
                    }
                }
                else
                {
                    if (index <= lastFixedCol)
                    {
                        for (int j = 0; j < index; j++)
                        {
                            if (!grid.GridHeaderList[j].Visible) continue;
                            left += grid.GridHeaderList[j].Width;
                        }
                    }
                    else
                    {
                        left += GetFixedColWidth();
                        for (int j = firstVisibleCol + lastFixedCol + 1; j < lastVisibleCol && j < index; j++)
                        {
                            if (!grid.GridHeaderList[j].Visible) continue;
                            left += grid.GridHeaderList[j].Width;
                        }
                    }
                }

                if (allRowsHeight > 0)
                {
                    if (colFixed == 0)
                    {
                        if (index >= firstVisibleCol)
                        {
                            g.FillRectangle(selectedColor, left + 1, topHeaderHeight, grid.GridHeaderList[index].Width + 1, allRowsHeight - (rowHeight * firstVisibleRow) + 1);
                        }
                    }
                    else
                    {
                        if (index > firstVisibleCol + lastFixedCol)
                        {
                            g.FillRectangle(selectedColor, left + 1, topHeaderHeight, grid.GridHeaderList[index].Width + 1, allRowsHeight - (rowHeight * firstVisibleRow) + 1);
                        }
                        else if (index <= lastFixedCol)
                        {
                            g.FillRectangle(selectedColor, left + 1, topHeaderHeight, grid.GridHeaderList[index].Width + 1, allRowsHeight - (rowHeight * firstVisibleRow) + 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Grid의 Header 그리기
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rc"></param>
        private void DrawHeaders(Graphics g, Rectangle rc, int colFixed, bool fixedColEditable)
        {
            if (grid != null)
            {
                grid.DrawHeader(g, rc, rc.Width, colFixed, fixedColEditable);                
            }
        }

        /// <summary>
        /// Grid의 내용 그리기
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rc"></param>
        private void DrawContent(Graphics g, Rectangle rc, int controlWidth)
        {
            SolidBrush brush = new SolidBrush(SystemColors.ControlLight);
            Pen pen = new Pen(Color.LightGray);

            if (rows.Count <= 0) return;
            g.Clip = new Region(new Rectangle(1, topHeaderHeight, Width - ysclWidth + 2, Height - xsclHeight - topHeaderHeight));
            
            int columnStartY = topHeaderHeight;
            for (int i = firstVisibleRow; i <= lastVisibleRow && i < rows.Count; i++)
            {
                int columnStartX = 0;
                int columnWidth = 0;

                //고정 컬럼이 없을 경우
                if (colFixed == 0)
                {
                    g.FillRectangle(brush, 1, columnStartY + 1, leftHeaderWidth, rowHeight);
                    g.DrawRectangle(pen, 1, columnStartY + 1, leftHeaderWidth, rowHeight);
                    columnStartX += leftHeaderWidth;  //첫 시작컬럼의 폭을 leftHeaderWidth 만큼 설정                                                                

                    for (int j = firstVisibleCol; j <= lastVisibleCol; j++)
                    {
                        Col col = new Col(this.grid.HeaderGen.GetHeaders(), rows[i].DataRow);
                        string content = col.GetColText(j);                        

                        if (!grid.GridHeaderList[j].Visible) continue;

                        //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                        if (columnStartX + grid.GridHeaderList[j].Width > controlWidth)
                        {
                            columnWidth = controlWidth - columnStartX - 3;
                            g.DrawRectangle(pen, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);                            
                            Rectangle rec = new Rectangle(columnStartX + 2, columnStartY + 2, columnWidth - 2, rowHeight);
                            DrawStringAlignment(content, rec, g, col.Alignment);
                        }
                        else
                        {
                            columnWidth = grid.GridHeaderList[j].Width;
                            g.DrawRectangle(pen, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);                            
                            Rectangle rec = new Rectangle(columnStartX + 2, columnStartY + 2, columnWidth - 2, rowHeight);
                            DrawStringAlignment(content, rec, g, col.Alignment);
                        }
                        columnStartX += columnWidth;
                    }

                    columnStartY += rowHeight;
                }
                else  //고정 컬럼이 있을 경우
                {
                    int fixCol = GetLastFixedCol();
                    Col col = new Col(this.grid.HeaderGen.GetHeaders(), rows[i].DataRow);
                    for (int index = 0; index <= fixCol; index++)
                    {
                        if (index == 0)
                        {
                            g.FillRectangle(brush, 1, columnStartY + 1, leftHeaderWidth, rowHeight);
                            g.DrawRectangle(pen, 1, columnStartY + 1, leftHeaderWidth, rowHeight);
                            columnStartX += leftHeaderWidth;  //첫 시작컬럼의 폭을 leftHeaderWidth 만큼 설정                                            
                        }

                        if (!col.ColHeaders[index].Visible)
                        {
                            continue;
                        }

                        string content = col.GetColText(index);                        

                        //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                        if (columnStartX + grid.GridHeaderList[index].Width > controlWidth)
                        {
                            columnWidth = controlWidth - columnStartX - 3;
                            //고정컬럼 Backgroud 색상을 colFixBrush로 채운다
                            g.FillRectangle(colFixBrush, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);
                            g.DrawRectangle(pen, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);
                            Rectangle rec = new Rectangle(columnStartX + 2, columnStartY + 2, columnWidth - 2, rowHeight);
                            DrawStringAlignment(content, rec, g, col.Alignment);
                        }
                        else
                        {
                            columnWidth = grid.GridHeaderList[index].Width;
                            //고정컬럼 Backgroud 색상을 colFixBrush로 채운다
                            g.FillRectangle(colFixBrush, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);
                            g.DrawRectangle(pen, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);
                            Rectangle rec = new Rectangle(columnStartX + 2, columnStartY + 2, columnWidth - 2, rowHeight);
                            DrawStringAlignment(content, rec, g, col.Alignment);
                        }
                        YearMonthWeekNoDayHeader yearMonthHeader = grid.GridHeaderList[index] as YearMonthWeekNoDayHeader;
                        columnStartX += columnWidth;
                    }                    

                    int lastFixCol = fixCol + 1;
                    for (int j = firstVisibleCol + lastFixCol; j <= lastVisibleCol && j < grid.GridHeaderList.Count; j++)
                    {
                        col = new Col(this.grid.HeaderGen.GetHeaders(), rows[i].DataRow);
                        string content = col.GetColText(j);
                        if (j == firstVisibleCol)
                        {
                            g.FillRectangle(brush, 1, columnStartY + 1, leftHeaderWidth, rowHeight);
                            g.DrawRectangle(pen, 1, columnStartY + 1, leftHeaderWidth, rowHeight);
                            columnStartX += leftHeaderWidth;  //첫 시작컬럼의 폭을 leftHeaderWidth 만큼 설정                                            
                        }

                        if (!grid.GridHeaderList[j].Visible) continue;

                        //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                        if (columnStartX + grid.GridHeaderList[j].Width > controlWidth)
                        {
                            columnWidth = controlWidth - columnStartX - 3;
                            g.DrawRectangle(pen, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);
                            Rectangle rec = new Rectangle(columnStartX + 2, columnStartY + 2, columnWidth - 2, rowHeight);
                            DrawStringAlignment(content, rec, g, col.Alignment);
                        }
                        else
                        {
                            columnWidth = grid.GridHeaderList[j].Width;
                            g.DrawRectangle(pen, columnStartX + 1, columnStartY + 1, columnWidth, rowHeight);
                            Rectangle rec = new Rectangle(columnStartX + 2, columnStartY + 2, columnWidth - 2, rowHeight);
                            DrawStringAlignment(content, rec, g, col.Alignment);
                        }
                        columnStartX += columnWidth;
                    }

                    columnStartY += rowHeight;
                }
            }            
        }

        /// <summary>
        /// FixedCol 수에 맞는 Header의 Index값을 리턴
        /// fixedCol 2이지만 실제 Header Column의 Visible 값이 false인 경우 제외하고 실제 Header Column의 Index를 구한다.
        /// </summary>
        /// <returns></returns>
        private int GetLastFixedCol()
        {
            if (grid.GridHeaderList == null) return -1;
            int lastFixedCol = 0;
            int startIndex = 0;
            foreach (Header head in grid.GridHeaderList)
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

        /// <summary>
        /// 고정 컬럼의 전체 폭을 구한다.
        /// </summary>
        /// <returns></returns>
        private int GetFixedColWidth()
        {
            int fixedColsWidth = 0;
            int startIndex = 0;
            foreach (Header head in grid.GridHeaderList)
            {
                if (startIndex == colFixed) break;
                if (head.Visible)
                {
                    fixedColsWidth += head.Width;
                    startIndex++;
                }
            }
            return fixedColsWidth;
        }

        /// <summary>
        /// Content 정렬에 맞추어 텍스트를 보여준다.
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="rec"></param>
        /// <param name="g"></param>
        /// <param name="align"></param>
        private void DrawStringAlignment(string txt, Rectangle rec, Graphics g, HorizontalAlignment align)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringUtil.GetStringAlignment(sf, align); //Header에서 정렬방식을 체크해서 StringFormat의 Alignment 반환
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(txt, Font, blackBrush, rec, sf);
        }

        /// <summary>
        /// Active Cell을 표기한다.
        /// </summary>
        /// <param name="g"></param>
        private void DrawActiveCell(Graphics g)
        {
            if (ActiveCell.Row != -1 && ActiveCell.Col != -1)
            {
                //파선 패턴 설정
                float[] dashValues = { 1, 1, 1, 1 };
                Pen grayPen = new Pen(Color.Gray, 1);
                grayPen.DashPattern = dashValues;
                g.DrawRectangle(grayPen, GetSelectedCellRect(ActiveCell.Row, ActiveCell.Col));
            }
        }

        /// <summary>
        /// 컬럼 사이즈 변경을 위해 Header영역의 컬럼과 컬럼 경계선을 선택하고 마우스 이동을 할때 표시되는 세로 선
        /// </summary>
        /// <param name="pos"></param>
        private void DrawVSpliteLine(Point pos)
        {
            int allHeight = topHeaderHeight + allRowsHeight;
            Graphics g = Graphics.FromHwnd(this.Handle);
            Rectangle rc = new Rectangle(lastMousePoint.X - 1, 1, 2, allHeight - (firstVisibleRow * rowHeight));
            Invalidate(rc);
            g.DrawLine(new Pen(Color.Gray, 2), pos.X, 1, pos.X, allHeight - (firstVisibleRow * rowHeight));
            lastMousePoint = pos;
            g.Dispose();
        }

        /// <summary>
        /// 선택한 Cell의 영역을 반환
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        protected Rectangle GetSelectedCellRect(int row, int col)
        {
            if (row < firstVisibleRow || row > lastVisibleRow) return new Rectangle(0, 0, 0, 0);
            if ((colFixed == 0 && col < firstVisibleCol) || col > lastVisibleCol) return new Rectangle(0, 0, 0, 0);

            //선택된 Cell의 높이를 구한다.
            int top = topHeaderHeight;
            int height = 0;
            for (int i = firstVisibleRow; i <= lastVisibleRow; i++)
            {
                if (rows.Count == 0)
                {
                    EndEdit();
                    continue;
                }
                height = rows[i].MaxLines * rowHeight;
                if (row == i) break;
                top += height;
            }

            int left = leftHeaderWidth + 2;
            int width = 0;

            if (colFixed == 0)
            { 
                for (int i = firstVisibleCol; i <= lastVisibleCol; i++)
                {
                    if (!grid.GridHeaderList[i].Visible) continue;

                    //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                    if (left + grid.GridHeaderList[i].Width > this.Width)
                    {
                        width = this.Width - left - 1;
                    }
                    else
                    {
                        width = grid.GridHeaderList[i].Width;
                    }

                    if (col == i) break;
                    if (i > col) return new Rectangle(0, 0, 0, 0);
                    left += width;
                }
            }
            else
            {
                int fixCol = GetLastFixedCol();
                for (int i = 0; i <= fixCol; i++)
                {
                    if (!grid.GridHeaderList[i].Visible) continue;
                    //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                    if (left + grid.GridHeaderList[i].Width > this.Width)
                    {
                        width = this.Width - left - 1;
                    }
                    else
                    {
                        width = grid.GridHeaderList[i].Width;
                    }
                    
                    if (col == i) break;
                    left += width;                    
                }

                if (col > fixCol)
                {
                    for (int i = firstVisibleCol + fixCol + 1; i <= lastVisibleCol; i++)
                    {
                        if (!grid.GridHeaderList[i].Visible) continue;

                        //보여지는 컬럼의 폭이 컨트롤의 폭 보다 클경우
                        if (left + grid.GridHeaderList[i].Width > this.Width)
                        {
                            width = this.Width - left - 1;
                        }
                        else
                        {
                            width = grid.GridHeaderList[i].Width;
                        }

                        if (col == i) break;
                        if (i > col) return new Rectangle(0, 0, 0, 0);
                        left += width;
                    }
                }
            }
            return new Rectangle(left - 1, top + 1, width - 1, height - 1);
        }

        /// <summary>
        /// 셀(Cell)에서 입력 시작
        /// </summary>
        private void BeginEdit()
        {
            if (readOnly) return;
            if (grid.GridHeaderList[ActiveCell.Col].Editable == false) return;
            if (ActiveCell.Col != -1 && ActiveCell.Row != -1)
            {
                string tempStr = "";
                if (rows[ActiveCell.Row].DataRow[grid.GridHeaderList[ActiveCell.Col].ColumnId] != null)
                {
                    tempStr = rows[ActiveCell.Row].DataRow[grid.GridHeaderList[ActiveCell.Col].ColumnId].ToString();
                }
                //TextBox에 입력된 값을 설정하고 TextBox 속성의 값을 설정한다.
                editBox.Text = tempStr;
                Rectangle r = GetSelectedCellRect(ActiveCell.Row, ActiveCell.Col);
                editBox.Left = r.Left + 3;
                editBox.Top = r.Top + 3;
                editBox.Height = r.Height;
                editBox.Width = r.Width - 3;
                editBox.Visible = true;
                editBox.TextAlign = grid.GridHeaderList[ActiveCell.Col].TextAlign;
                editBox.Focus();
                ActiveCell_ActvieCol = ActiveCell.Col; //ActivieCell_ActiveCol 값을 설정   
            }
        }

        /// <summary>
        /// 셀(Cell)에서 입력 완료
        /// </summary>
        private void EndEdit()
        {
            if (readOnly) return;
            if (ActiveCell.Col != -1 && ActiveCell.Row != -1 && editBox.Visible)
            {
                ActiveCell_ActvieCol = -1;
            }
            editBox.Visible = false;
            editBox.Text = "";
            gridState = GridState.ACTIVE;
        }

        /// <summary>
        /// Row 추가
        /// </summary>
        public void AppendRow()
        {
            DataRow row = dataSource.NewRow();
            Row r = new Row(row);
            rows.Add(r);
            dataSource.Rows.Add(row);
            rowHeight = Font.Height + 4;
            allRowsHeight += rowHeight;
            OnRowsChanged();
        }

        /// <summary>
        /// Row 삭제
        /// </summary>
        /// <param name="delRow"></param>
        public void DeleteRow(int delRow)
        {
            if (delRow < rows.Count && delRow >= 0)
            {
                DataRow row = rows[delRow].DataRow; //전체 Row중에 선택 Row를 가져온다.
                row.Delete();   //해당 Row 정보를 delete한다. 
                rows.RemoveAt(delRow);  //전체 Row중에 선택된 Row를 제거한다.      
                allRowsHeight -= rowHeight; //전제 Row의 높이에서 선택된 Row의 높이를 뺀다
                OnRowsChanged();
            }
        }

        /// <summary>
        /// 1개의 Row 삭제 후 선택한 행들의 Index 정보를 갱신한다.
        /// </summary>
        /// <param name="index"></param>
        public void RebuildSelectedRowsIndex(int index)
        {
            for (int i = index; i < selectedRows.Count; i++)
            {
                int reIndex = selectedRows[i] - 1;
                if (reIndex > -1) selectedRows[i] = reIndex;
            }
        }

        /// <summary>
        /// 이전 행에 Row 추가
        /// </summary>
        /// <param name="crntRow"></param>
        public void BeforeInsert(int crntRow)
        {
            DataRow row = dataSource.NewRow();  //DataTable인 dataSource에 새로운 행의 DataRow 생성
            Row r = new Row(row);   //새로운 Row를 생성

            if (crntRow < 0) crntRow = 0;   //crntRow의 값이 0보다 작으면 0으로 설정
            rows.Insert(crntRow, r);    //전제 Row를 관리하는 List인 rows에 생성된 Row를 추가 
            dataSource.Rows.InsertAt(row, crntRow); //생성된 DataRow를 DataTable에 원하는 위치에 Insert
            allRowsHeight += Font.Height + 4;   //전체 Row의 높이 값을 저장하는 allRowsHeight에 추가되는 Row의 높이 값을 더한다.
            rowHeight = Font.Height + 4;    //1개 Row의 높이 값을 rowHeight 변수에 저장
            OnRowsChanged();
        }

        /// <summary>
        /// 다음 행에 Row 추가
        /// </summary>
        /// <param name="crntRow"></param>
        public void AfterInsert(int crntRow)
        {
            if (crntRow + 1 == rows.Count) //현재 행(Row)가 마지막 행(Row)일 경우
            {
                AppendRow();
            }
            else
            {
                DataRow row = dataSource.NewRow();  //DataTable인 dataSource에 새로운 행의 DataRow 생성
                Row r = new Row(row);    //새로운 Row를 생성
                rows.Insert(crntRow + 1, r);    //핸재 행의 다음행에 추가
                dataSource.Rows.InsertAt(row, crntRow + 1);     //생성된 DataRow를 DataTable의 현재행 다음에 Insert
                allRowsHeight += Font.Height + 4;   //전체 Row의 높이 값을 저장하는 allRowsHeight에 추가되는 Row의 높이 값을 더한다.
                rowHeight = Font.Height + 4;    //1개 Row의 높이 값을 rowHeight 변수에 저장
                OnRowsChanged();
            }
        }

        /// <summary>
        /// DataTable을 WANIGrid에 설정한다.
        /// </summary>
        /// <param name="dt"></param>
        public void SetDataTable(DataTable dt)
        {
            if (dt == null) return;
            if (dataSource.Rows.Count > 0)
            {             
                rows.Clear();
                allRowsHeight = 0;
            }
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                Row r = new Row(row);
                rows.Insert(count, r);
                if (rowHeight == 0) rowHeight = Font.Height + 4;
                allRowsHeight += rowHeight;
                count++;
            }
            Invalidate();
        }

        /// <summary>
        /// 행(Row)이 추가 또는 삭제 되었을 때 호출. 행(Row) 변화에 맞춰 화면을 새로 그리도록 한다.
        /// </summary>
        public void OnRowsChanged()
        {
            ReCalcScrollBars();
            CalcVisibleRange();
            Invalidate(true);
        }

        /// <summary>
        /// Context Menu를 보여준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowContextMenu(object sender, MouseEventArgs e)
        {
            if (!isShowContextMenu) return;
            //마우스 우측 버튼이 클릭된 좌표 값을 얻어온다.
            Point p = new Point(e.X, e.Y);

            if (selectedRows.Count <= 0)
            {
                rightClickMenu.MenuItems[0].Enabled = false;
                rightClickMenu.MenuItems[1].Enabled = false;
                rightClickMenu.MenuItems[2].Enabled = true;
                rightClickMenu.MenuItems[3].Enabled = false;
            }
            else if (selectedRows.Count == 1)
            {
                rightClickMenu.MenuItems[0].Enabled = true;
                rightClickMenu.MenuItems[1].Enabled = true;
                rightClickMenu.MenuItems[2].Enabled = true;
                rightClickMenu.MenuItems[3].Enabled = true;
            }
            else if (selectedRows.Count > 1)
            {
                rightClickMenu.MenuItems[0].Enabled = false;
                rightClickMenu.MenuItems[1].Enabled = false;
                rightClickMenu.MenuItems[2].Enabled = true;
                rightClickMenu.MenuItems[3].Enabled = true;
            }

            rightClickMenu.Show(this, p);
        }

        /// <summary>
        /// 선택한 행(Row)을 selectedRows에 추가 하거나 제거한다.
        /// </summary>
        /// <param name="row"></param>
        private void SelectedRowsChangeColor(int row)
        {
            if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift)
            {
                if (selectedRows.Contains(row))
                {
                    if (selectedRows.Count > 1) //선택된 행(Row)이 2개 이상일 경우
                    {
                        selectedRows.Clear();   //여러 행(Row)가 선택된 경우 기존의 선택된 행(Row) 무효화
                        selectedRows.Add(row);  //선택 행(Row) 추가
                    }
                    else selectedRows.Remove(row);  //동일한 행(Row)를 2번 선택하면 선택 표시 지움
                }
                else //선택된 행(Row)가 없을 경우 기존 선택 행(Row)를 모두 지우고 선택한 행(Row)를 추가
                {
                    selectedRows.Clear();
                    selectedRows.Add(row);
                }
            }
            else
            {
                if (Control.ModifierKeys == Keys.Shift && selectedRows.Count > 0)
                {
                    int index = selectedRows[0];
                    int begin = Math.Min(row, index);
                    int end = Math.Max(row, index);
                    selectedRows.Clear();
                    for (int i = begin; i <= end; i++)
                    {
                        selectedRows.Add(i);
                    }
                }
                else if (Control.ModifierKeys == Keys.Control)
                {
                    if (selectedRows.Contains(row)) selectedRows.Remove(row);   //선택된 행(Row)을 다시 선택할 경우 제거해서 행(Row) 선택 무효화
                    else selectedRows.Add(row); //선택된 행(Row)를 추가
                }
            }
            //Row를 선택하면 EditBox를 감춘다.
            editBox.Visible = false;
        }
        #endregion Method
    }
}
