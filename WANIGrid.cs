using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WANI_Grid.Grid;
using WANI_Grid.Grid.Head;
using WANI_Grid.Resources;
using System.ComponentModel;
using WANI_Grid.Grid.Element;
using System.Collections.Generic;
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
    public enum GridState
    {
        NONE,
        ACTIVE,
        EDIT
    }

    public partial class WANIGrid : UserControl
    {
        #region 변수
        private int topHeaderHeight = 20;   //Grid의 Header 높이
        private WANI_Grid.Grid.Grid grid = new WANI_Grid.Grid.Grid();
        private Rectangle rc;
        private GridType grdType = GridType.DefaultType;
        private int xsclHeight = 0;
        private int ysclWidth = 0;
        private int firstVisibleCol = 0;    //화면상에서 처음 보여져야할 컬럼
        private int lastVisibleCol = 0;     //화면상에서 마지막에보여질 컬럼    
        private int firstVisibleRow = 0;    //화면상에서 처음 보여져야할 로우
        private int lastVisibleRow = 0;     //화면상에서 마지막에 보여질 로우
        private int leftHeaderWidth = 22; //Grid의 맨 왼쪽 빈Column Width
        private int allColsWidth = 0;   //Grid 컬럼 전체의 폭
        private int allRowsHeight = 0;  //Grid Row의 전체 높이  
        private int lastHScrollValue = 0; //가로 스크롤바를 최대값까지 갔을 경우 보여지는 컬럼 수를 2로 나눈 값
        private bool chkLast = false; //마지막 컬럼의 폭을 제대로 보여주었을 때 true
        private ContextMenu rightClickMenu = null;  //Mouse 우측 버튼 클릭 시 제공되는 메뉴
        private Font headerFont = new Font("맑은 고딕", 9, FontStyle.Bold);
        private Font contentFont = new Font("맑은 고딕", 9);
        private int rowHeight = 0;
        private RowCollection rows;
        private DataTable dataSource;
        private int rowsCount = 0;  //Grid Control에서 보여지는 행의 갯수를 저장하기 위한 변수        
        private List<int> selectedCols = new List<int>();   //선택된 컬럼(Columns)들을 관리하기 위한 변수
        private List<int> selectedRows = new List<int>();   //선택된 행(Rows)들을 관리하기 위한 변수
        private Point mousePoint = new Point(0, 0);
        private SolidBrush selectedColor = new SolidBrush(Color.LightCyan);
        private Cell ActiveCell = new Cell(-1, -1);
        private int ActiveCell_ActiveRow = 0;
        private int ActiveCell_ActvieCol = -1;
        private bool readOnly = false;
        private TextBox editBox = null;
        private GridState gridState = GridState.NONE;
        private SolidBrush blackBrush = new SolidBrush(Color.Black);
        private bool vSpliteLineMouseDown = false;  //컬럼 경계선 상에서 마우스 좌측버튼이 눌러졌는지를 저장하기 위한 변수
        private int resizeCol = 0;  //사이즈 변경이 발생한 컬럼을 저장하기 위한 변수
        private Point lastMousePoint = new Point(0, 0); //마우스 좌측 버튼을 누른 상태에서 마지막 이동 Point를 저장하기 위한 변수
        #endregion

        #region Property
        /// <summary>
        /// Grid Class 설정
        /// </summary>
        public WANI_Grid.Grid.Grid Grid
        {
            get { return grid; }
            set { grid = value; }
        }

        /// <summary>
        /// GridType 설정
        /// </summary>
        public GridType GridDisplayType
        {
            get { return grdType; }
            set { grdType = value; }
        }

        /// <summary>
        /// Grid 왼쪽 빈공간 폭 설정
        /// </summary>
        public int LeftHeaderWidth
        {
            get { return leftHeaderWidth; }
            set
            {
                leftHeaderWidth = value;
                if (grid == null) grid = new WANI_Grid.Grid.Grid();
                if (grid.HeaderGen != null) grid.HeaderGen.LeftHeaderWidth = leftHeaderWidth;
            }
        }

        public DataTable DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }

        [Category("Font"), Description("The header font")]
        public Font HeaderFont
        {
            get { return headerFont; }
            set
            {
                if (headerFont != value)
                {
                    headerFont = value;
                    topHeaderHeight = headerFont.Height + 4;
                }
            }
        }

        [Category("Font"), Description("The Content font")]
        public new Font Font
        {
            get { return contentFont; }
            set
            {
                if (contentFont != value)
                {
                    contentFont = value;
                    rowHeight = Font.Height + 4;
                }
            }
        }
        
        /// <summary>
        /// 선택된 행과 컬럼의 색상을 변경
        /// </summary>
        public Color SelectedColor
        {            
            set { selectedColor = new SolidBrush(value); }
        }
        #endregion Property

        #region 생성자
        public WANIGrid()
        {
            InitializeComponent();
            if (grid == null) grid = new WANI_Grid.Grid.Grid();
            rows = new RowCollection();
            hScrollBar.SmallChange = 1;
            rowHeight = Font.Height + 4;
            vScrollBar.SmallChange = rowHeight;
            editBox = new TextBox();
            editBox.BorderStyle = BorderStyle.None;
            editBox.BackColor = Color.White;
            editBox.Font = Font;
            editBox.Visible = false;
            Controls.Add(editBox);
            //마우스 우측 버튼 클릭 시 제공되는 ContextMenu 초기화
            InitializeContextMenu();
        }
        #endregion 생성자

        #region 초기화
        /// <summary>
        /// HScrollBar, VScrollBar 초기화
        /// </summary>
        public void InitializeScollBar()
        {
            rowsCount = 0; //Control 크기가 변경되었을 경우 행(Row) 개수 초기화
            grid.FirstVisibleCol = 0;
            grid.LastVisibleCol = 0;
            grid.FirstVisibleRow = 0;
            grid.LastVisibleRow = 0;

            //가로 스크롤바 설정
            hScrollBar.Left = 1;
            hScrollBar.Width = Width - vScrollBar.Width - 2;
            hScrollBar.Top = Height - hScrollBar.Height - 2;

            //세로 스크롤바 설정
            vScrollBar.Left = Width - vScrollBar.Width - 2;
            vScrollBar.Top = topHeaderHeight + 2;
            vScrollBar.Height = Height - topHeaderHeight - hScrollBar.Height - 4;            
        }

        /// <summary>
        /// 마우스 우측 버튼 클릭시 제공 되는 메뉴 초기화
        /// </summary>
        private void InitializeContextMenu()
        {
            //System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US"); // 한국어 : "ko-KR"
            //LanguageResource.Culture = cultureInfo;
            rightClickMenu = new ContextMenu();
            rightClickMenu.MenuItems.Add(LanguageResource.Row_Before_Insert);
            rightClickMenu.MenuItems.Add(LanguageResource.Row_After_Insert);
            rightClickMenu.MenuItems.Add(LanguageResource.Row_Append);
            rightClickMenu.MenuItems.Add(LanguageResource.Row_Delete);

            rightClickMenu.MenuItems[2].Click += new EventHandler(OnMenu_AppenRow_Click);
        }

        /// <summary>
        /// Event Handler 초기화
        /// </summary>
        public void EventHandlerInitialize()
        {
            //가로 스크롤바
            hScrollBar.Scroll += new ScrollEventHandler(HScrollBar_Scroll);
            //세로 스크롤바
            vScrollBar.Scroll += new ScrollEventHandler(VScrollBar_Scroll);
            //마우스 휠
            this.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            //editBox TextChanged 이벤트
            this.editBox.TextChanged += EditBox_TextChanged;
        }

        /// <summary>
        /// TextBox인 editBox의 TextChanged Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBox_TextChanged(object sender, EventArgs e)
        {
            DataRow row = rows[ActiveCell.Row].DataRow;
            Header header = grid.GridHeaderList.Where(x => x.Index == ActiveCell.Col).FirstOrDefault();
            if (header != null)
            {
                if (ActiveCell_ActvieCol > -1) row[header.ColumnId] = editBox.Text;
            }
        }

        /// <summary>
        /// HScrollBar의 Scroll Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            EndEdit();
            //가로 스크롤바를 움직여서 마지막 컬럼이 Client 영역에 나타났을 때
            if (e.NewValue >= (grid.GridHeaderList.Count - lastHScrollValue))
            {
                //마지막 컬럼의 전체가 Client 영역에 모두 나타나지 않았을 때
                if (grid.HeaderGen.IsLargeLastCol)
                {
                    //마지막 컬럼을 1칸 앞으로 땡긴다.
                    if (grid.LastVisibleCol >= grid.GridHeaderList.Count - 1)
                    {
                        if (!chkLast)
                        {
                            firstVisibleCol += 1;
                            grid.FirstVisibleCol = firstVisibleCol;
                            e.NewValue = firstVisibleCol;
                            chkLast = true;
                        }
                    }
                }
                else
                {
                    if (e.NewValue <= grid.GridHeaderList.Count - lastHScrollValue && grid.LastVisibleCol != (grid.GridHeaderList.Count - 1))
                    {
                        firstVisibleCol = e.NewValue;
                        grid.FirstVisibleCol = firstVisibleCol;
                        chkLast = false;
                    }
                }
            }
            else
            {
                if (e.NewValue < (grid.GridHeaderList.Count - lastHScrollValue))
                {
                    if (firstVisibleCol < grid.LastVisibleCol && grid.LastVisibleCol != (grid.GridHeaderList.Count - 1))
                    {
                        firstVisibleCol = e.NewValue;
                        grid.FirstVisibleCol = firstVisibleCol;
                        lastHScrollValue = grid.LastVisibleCol;
                        chkLast = false;
                    }
                    else if (e.NewValue < firstVisibleCol)
                    {
                        firstVisibleCol = e.NewValue;
                        grid.FirstVisibleCol = firstVisibleCol;
                        chkLast = false;
                    }
                }
            }
            CalcVisibleRange();
            ReCalcScrollBars();
            Invalidate();
        }

        /// <summary>
        /// VScrollBar의 Scroll Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            EndEdit();
            firstVisibleRow = e.NewValue / rowHeight;
            if (firstVisibleRow > (allRowsHeight / rowHeight)) return;

            if (firstVisibleRow >= (allRowsHeight / rowHeight) - rowsCount)
            {
                firstVisibleRow = (allRowsHeight / rowHeight) - rowsCount;
                grid.FirstVisibleRow = firstVisibleRow;
                vScrollBar.Value = vScrollBar.Maximum;
            }
            else
            {
                grid.FirstVisibleRow = firstVisibleRow;
                vScrollBar.Value = firstVisibleRow * rowHeight;
            }

            CalcVisibleRange();
            ReCalcScrollBars();
            Invalidate();
        }

        /// <summary>
        /// Mouse Wheel 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            EndEdit();
            //Control Key를 누르고 Wheel을 돌렸을 경우는 HScrollBar와 동일
            if (ModifierKeys == Keys.Control)
            {
                if ((e.Delta / 120) > 0)  //업의 경우에는 좌측으로 이동
                {
                    firstVisibleCol -= 2;
                    if (firstVisibleCol < 0) firstVisibleCol = 0;
                    grid.FirstVisibleCol = firstVisibleCol;
                    hScrollBar.Value = firstVisibleCol;
                }
                else //다운의 경우에는 우측으로 이동
                {
                    firstVisibleCol += 2;
                    if (firstVisibleCol >= (grid.GridHeaderList.Count - 1)) firstVisibleCol = grid.GridHeaderList.Count - 3;
                    grid.FirstVisibleCol = firstVisibleCol;
                    hScrollBar.Value = hScrollBar.Maximum;
                }
            }
            else //Wheel만 움직였을 경우에는 VScrollBar와 동일
            {
                if (firstVisibleRow < 0) return;

                if ((e.Delta / 120) > 0) //업의 경우 위쪽으로 이동
                {
                    firstVisibleRow -= 2;
                    if (firstVisibleRow < 0)
                    {
                        firstVisibleRow = 0;
                        grid.FirstVisibleRow = firstVisibleRow;
                        vScrollBar.Value = 0;
                    }
                    else
                    {
                        grid.FirstVisibleRow = firstVisibleRow;
                        vScrollBar.Value = firstVisibleRow * rowHeight;
                    }
                }
                else //다운의 경우에는 아래쪽으로 이동
                {
                    if (rowsCount == 0) return; // rowsCount가 0일 경우는 Row의 Height가 Control Height를 넘지 않았음
                    firstVisibleRow += 2;
                    if (firstVisibleRow >= (allRowsHeight / rowHeight) - rowsCount)
                    {
                        firstVisibleRow = (allRowsHeight / rowHeight) - rowsCount;
                        grid.FirstVisibleRow = firstVisibleRow;
                        vScrollBar.Value = vScrollBar.Maximum;
                    }
                    else
                    {
                        grid.FirstVisibleRow = firstVisibleRow;
                        vScrollBar.Value = firstVisibleRow * rowHeight;
                    }
                }
            }
            CalcVisibleRange();
            ReCalcScrollBars();
            Invalidate();
        }
        #endregion 초기화

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
                for (col = firstVisibleCol; col < lastVisibleCol; col++)
                {
                    if (!grid.GridHeaderList[col].Visible) continue;
                    int width = grid.GridHeaderList[col].Width;
                    if (X < width + tempWidth) break;
                    tempWidth += width;
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
            if (headerBuilder != null) grid.HeaderGen = headerBuilder.HeaderGen;

            //dataSource가 null일 경우 Header정보를 근간으로 DataTable 생성
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

            //컬럼의 폭이 클라이언트 사이즈 폭 보다 클 경우 가로 스크롤바를 보여준다.
            if ((allColsWidth > 0) && (allColsWidth > ClientSize.Width - ysclWidth))
            {
                hScrollBar.Visible = true;
                hScrollBar.LargeChange = ((lastVisibleCol - firstVisibleCol) + 1) / 2 + 1;
                lastHScrollValue = ((lastVisibleCol - firstVisibleCol) + 1) / 2 + 1;
            }
            else
            {
                hScrollBar.Visible = false;
                grid.FirstVisibleCol = 0; //Control 크기가 바뀌면서 hScrollBar가 가려지면 Grid의 첫번째 컬럼 부터 그려지도록 처리
            }
            //로우의 높이가 클라이언트 사이즈 높이 보다 클 경우 세로 스크롤바를 보여준다.
            if (allRowsHeight > 0 && (allRowsHeight > Height - topHeaderHeight - xsclHeight))
            {
                vScrollBar.Visible = true;
                if (rowsCount == 0) rowsCount = (allRowsHeight / rowHeight) - 1;
                vScrollBar.Maximum = allRowsHeight;
                vScrollBar.LargeChange = rowHeight * 5;
                vScrollBar.SmallChange = rowHeight;
            }
            else
            {
                vScrollBar.Visible = false;
                grid.FirstVisibleRow = 0;
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

                if (grid.GridHeaderList.Count > 0)
                {
                    Header lastHeaderColumn = grid.GridHeaderList.Where(x => x.Visible == true).Last();
                    for (i = firstVisibleCol, tempPos = 0; i < grid.GridHeaderList.Count && tempPos < Width - ysclWidth - leftHeaderWidth; i++)
                    {
                        if (grid.GridHeaderList[i].Visible) tempPos += grid.GridHeaderList[i].Width;
                    }

                    lastVisibleCol = i - 1;
                    grid.LastVisibleCol = lastVisibleCol;
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

            //선택된 컬럼의 Background를 그린다.
            for (int i = 0; i < selectedCols.Count; i++)
            {
                int index = selectedCols[i];
                int left = leftHeaderWidth;

                for (int j = firstVisibleCol; j < lastVisibleCol && j < index; j++)
                {
                    if (!grid.GridHeaderList[j].Visible) continue;
                    left += grid.GridHeaderList[j].Width;
                }

                if (allRowsHeight > 0)
                {
                    g.FillRectangle(selectedColor, left + 1, topHeaderHeight, grid.GridHeaderList[index].Width + 1, allRowsHeight - (rowHeight * firstVisibleRow) + 1);
                }
            }

            //선택된 행(Row)의 Background를 그린다.
            for (int i = 0; i < selectedRows.Count; i++)
            {
                int index = selectedRows[i];                
                int top = topHeaderHeight;
                int width = 0;

                for (int k = firstVisibleCol; k <= lastVisibleCol; k++)
                {
                    width += grid.GridHeaderList[k].Width;
                }

                for (int j = firstVisibleRow; j < lastVisibleRow && j < index; j++)
                {
                    top += rows[j].MaxLines * rowHeight;
                }
                g.FillRectangle(selectedColor, leftHeaderWidth + 1, top + 1, width, rowHeight);
            }
        }        

        /// <summary>
        /// Grid의 Header 그리기
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rc"></param>
        private void DrawHeaders(Graphics g, Rectangle rc)
        {
            if (grid != null)
            {
                grid.DrawHeader(g, rc, rc.Width);
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

            try
            {
                int columnStartY = topHeaderHeight;
                for (int i = firstVisibleRow; i <= lastVisibleRow && i < rows.Count; i++)
                {
                    int columnStartX = 0;
                    int columnWidth = 0;

                    for (int j = firstVisibleCol; j <= lastVisibleCol && j < grid.GridHeaderList.Count; j++)
                    {                        
                        Col col = new Col(this.grid.HeaderGen.GetHeaders(), rows[i].DataRow);
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
                g.DrawRectangle(new Pen(Color.FromName("HotTrack"), 1), GetSelectedCellRect(ActiveCell.Row, ActiveCell.Col));                
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
            if (col < firstVisibleCol || col > lastVisibleCol) return new Rectangle(0, 0, 0, 0);

            //선택된 Cell의 높이를 구한다.
            int top = topHeaderHeight;
            int height = 0;
            for (int i = firstVisibleRow; i <= lastVisibleRow; i++)
            {
                height = rows[i].MaxLines * rowHeight;
                if (row == i) break;
                top += height;
            }

            int left = leftHeaderWidth + 2;
            int width = 0;
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
                left += width;
            }
            
            return new Rectangle(left - 1, top + 1, width - 1, height - 1);
        }

        private void BeginEdit()
        {
            if (readOnly) return;

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
                editBox.Focus();
                ActiveCell_ActvieCol = ActiveCell.Col; //ActivieCell_ActiveCol 값을 설정   
            }
        }

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
            allRowsHeight += Font.Height + 4;
            rowHeight = Font.Height + 4;
            OnRowsChanged();
        }

        public void OnRowsChanged()
        {
            ReCalcScrollBars();
            CalcVisibleRange();
            Invalidate(true);
        }
        #endregion Method

        #region Event
        protected void OnMenu_AppenRow_Click(object sender, EventArgs e)
        {
            AppendRow();
        }

        private void WANIGrid_Load(object sender, EventArgs e)
        {
            //더블버퍼링(Double Buffering) 처리
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            rc = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            EventHandlerInitialize(); //스크롤 이벤트 초기화
            InitializeScollBar();
            Invalidate();
        }

        private void WANIGrid_Resize(object sender, EventArgs e)
        {
            rc = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            ActiveCell.Clear();
            InitializeScollBar();
            ReCalcScrollBars();
            Invalidate();
        }

        private void WANIGrid_Paint(object sender, PaintEventArgs e)
        {
            CalcVisibleRange();
            ReCalcScrollBars();
            DrawBackground(e.Graphics, rc);
            DrawHeaders(e.Graphics, rc);
            DrawContent(e.Graphics, rc, this.ClientRectangle.Width);
            DrawActiveCell(e.Graphics);
        }

        private void WANIGrid_SizeChanged(object sender, EventArgs e)
        {
            rc = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            ActiveCell.Clear();
            InitializeScollBar();
            ReCalcScrollBars();
            Invalidate();
        }

        private void WANIGrid_ClientSizeChanged(object sender, EventArgs e)
        {
            rc = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            ActiveCell.Clear();
            InitializeScollBar();
            ReCalcScrollBars();
            Invalidate();
        }

        private void WANIGrid_MouseDown(object sender, MouseEventArgs e)
        {
            //마우스 우측 버튼 클릭 시 Context 메뉴 제공
            if (e.Button == MouseButtons.Right)
            {
                //WANIGrid Header 영역이 선택되어졌을 경우에는 메뉴 제공하지 않음.
                if (e.Y < grid.TopHeaderHeight) return;
                //마우스 우측 버튼이 클릭된 좌표 값을 얻어온다.
                Point p = new Point(e.X, e.Y);

                rightClickMenu.Show(this, p);
            }
            else if (e.Button == MouseButtons.Left)
            {
                //WANIGrid Top Header 영역을 마우스 좌측 버튼으로 클릭했을 때
                if (e.Y < topHeaderHeight)
                {
                    //Cursor가 Cursors.VSplite로 변경되었을 경우 vSpliteLineMouseDown을 true로 변경
                    if (Cursor == Cursors.VSplit)
                    {
                        vSpliteLineMouseDown = true;
                    }
                    else
                    {
                        vSpliteLineMouseDown = false;
                        MouseLeftButtonClickInTopHeadHeight(sender, e);
                        Invalidate();
                    }
                    //Header 영역에서 왼쪽 마우스 버튼이 눌려졌을 때 마우스 포인트 저장.
                    mousePoint.X = e.X;
                    mousePoint.Y = e.Y;
                }
                else //WANIGrid의 Top Header 영역을 제외한 영역에서 마우스 좌측 버튼을 클릭했을 때
                {
                    MouseLeftButtonClickInContents(sender, e);
                    Invalidate();
                }                
            }
        }

        /// <summary>
        /// WANIGrid의 Header 영역에서 마우스 좌측 버튼 클릭 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseLeftButtonClickInTopHeadHeight(object sender, MouseEventArgs e)
        {
            selectedRows.Clear();

            //컬럼이 존재할 경우
            if (grid.GridHeaderList.Count > 0)
            {
                int col = GetColFromX(e.X);
                if (col < 0) return; //col값이 -1이면 처리하지 않음                

                //Control Key를 누르지 않은 상태에서 컬럼을 선택했을 경우
                if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift)
                {
                    //선택된 컬럼일 경우
                    if (selectedCols.Contains(col))
                    {
                        if (selectedCols.Count > 1) //선택된 컬럼이 두 개 이상일 경우
                        {
                            selectedCols.Clear(); //여러 컬럼이 선택된 경우 기존의 선택된 컬럼 무효화
                            selectedCols.Add(col); //선택 컬럼 추가
                        }
                        else selectedCols.Remove(col);  //동일한 컬럼을 2번 선택하면 선택 표시 지움
                    }
                    else //선택된 컬럼이 없을 경우 기존 선택 컬럼을 모두 지우고 선택한 컬럼을 추가
                    {
                        selectedCols.Clear();
                        selectedCols.Add(col);
                    }
                }
                else
                {
                    if (Control.ModifierKeys == Keys.Shift && selectedCols.Count > 0)
                    {
                        int index = selectedCols[0];
                        int begin = Math.Min(col, index);
                        int end = Math.Max(col, index);
                        selectedCols.Clear();
                        for (int i = begin; i <= end; i++)
                        {
                            selectedCols.Add(i);
                        }
                    }
                    else if (Control.ModifierKeys == Keys.Control)
                    {
                        if (selectedCols.Contains(col)) selectedCols.Remove(col);   //선택된 컬럼을 다시 선택할 경우 제거해서 컬럼 선택 무효화
                        else selectedCols.Add(col); //선택된 컬럼을 추가
                    }
                }                
            }                     
        }

        /// <summary>
        /// WANIGrid Control의 맨 왼쪽 컬럼에서 마우스 좌측 버튼을 눌렀을 경우
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseLeftButtonClickInContents(object sender, MouseEventArgs e)
        {
            selectedCols.Clear();            
            int row = GetRowFromY(e.Y);
            int col = GetColFromX(e.X);
            if (row < 0) return;    //row값이 -1이면 처리하지 않음
            if (e.X < leftHeaderWidth)  //맨 좌측의 첫 컬럼을 선택했을 시 Row를 선택하도록 처리
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
            }
            else //WANIGrid Control 내부의 Content 영역을 마우스 좌측 버튼으로 클릭했을 때
            {
                selectedRows.Clear();
                selectedCols.Clear();

                if (row == ActiveCell.Row && col == ActiveCell.Col)
                {
                    Rectangle r = GetSelectedCellRect(ActiveCell.Row, ActiveCell.Col);
                    int k = 0;                    
                    for (int i = r.Top; i < r.Bottom && k < rows[row].MaxLines - 1; i *= rowHeight, k++)
                    {
                        if (e.Y >= i && e.Y <= i + rowHeight) break;
                    }
                    if (ActiveCell_ActiveRow != k) EndEdit();
                    ActiveCell_ActiveRow = k;
                    BeginEdit();    
                }
                else
                {
                    ActiveCell.Row = row;
                    ActiveCell.Col = col;
                    EndEdit(); 
                }
            }
        }
        #endregion Event                        

        private void WANIGrid_MouseUp(object sender, MouseEventArgs e)
        {
            //컬럼 사이즈 변경이 완료 되었을 경우 현재 컬럼의 Width 값을 다시 계산해서 저장한다.
            if (vSpliteLineMouseDown)
            {
                int width = e.X - mousePoint.X;
                grid.GridHeaderList[resizeCol].Width += width;
                Invalidate();

                mousePoint.X = 0;
                mousePoint.Y = 0;
            }
            //vSpliteLineMouseDown 값을 false, Cursor를 Cursors.Default로 설정
            vSpliteLineMouseDown = false;
            Cursor = Cursors.Default;
        }

        private void WANIGrid_MouseMove(object sender, MouseEventArgs e)
        {
            //vSpliteLineMouseDown이 true 이면 마우스 버튼이 눌러진 상태에서 이동 중인 상태임.
            if (vSpliteLineMouseDown)
            {
                if (Math.Abs(e.X - lastMousePoint.X) > 4) DrawVSpliteLine(new Point(e.X, e.Y));
                return;
            }

            //WANIGrid Header영역의 마우스 위치를 체크 - 컬럼과 컬럼 사이의 경계선에 위치하면 Cursors.VSplit로 변경하고 resizeCol을 확인한다.
            Cursor = Cursors.Default;
            if (grid.GridHeaderList.Count > 0 && e.Y < topHeaderHeight)
            {
                int colLine = leftHeaderWidth; //각 컬럼이 끝나는 지점의 X좌표를 저장하는 변수
                for (int i = firstVisibleCol; i <= lastVisibleCol; i++)
                {
                    if (!grid.GridHeaderList[i].Visible) continue;
                    colLine += grid.GridHeaderList[i].Width;
                    //Header의 컬럼과 컬럼 간의 경계선 상에 마우스 포인트가 위치했을 경우
                    if (e.X > colLine - 2 && e.X < colLine + 2)
                    {
                        Cursor = Cursors.VSplit;
                        resizeCol = i;
                        break;
                    }                    
                }
            }
        }
    }
}