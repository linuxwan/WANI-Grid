using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WANI_Grid.Grid.Head;
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
        #region Event
        /// <summary>
        /// Append Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMenu_AppenRow_Click(object sender, EventArgs e)
        {
            AppendRow();
        }

        /// <summary>
        /// 선택한 행(Row) 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMenu_DeleteRow_Click(object sender, EventArgs e)
        {
            if (selectedRows.Count > 0)
            {
                for (int i = 0; i < selectedRows.Count; i++)
                {
                    int crntRow = selectedRows[i];
                    DeleteRow(crntRow);
                    RebuildSelectedRowsIndex(crntRow);
                }
                selectedRows.Clear();
            }
        }

        /// <summary>
        /// 선택 Row 앞에 행(Row)을 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMenu_BeforeInsertRow_Click(object sender, EventArgs e)
        {
            int crntRow = ActiveCell.Row;
            BeforeInsert(crntRow);
        }

        /// <summary>
        /// 선택 Row 뒤에 행(Row)을 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMenu_AfterInsertRow_Click(object sender, EventArgs e)
        {
            int crntRow = ActiveCell.Row;
            AfterInsert(crntRow);
        }

        /// <summary>
        /// WANIGrid Control Load 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// WANIGrid Resize 시 발생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WANIGrid_Resize(object sender, EventArgs e)
        {
            rc = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            ActiveCell.Clear();
            InitializeScollBar();
            ReCalcScrollBars();
            Invalidate();
        }

        /// <summary>
        /// WANIGrid Paint Event - 원하는 시점에 화면 출력을 하고자 할 경우
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WANIGrid_Paint(object sender, PaintEventArgs e)
        {
            CalcVisibleRange();
            ReCalcScrollBars();
            DrawBackground(e.Graphics, rc);
            DrawHeaders(e.Graphics, rc);
            DrawContent(e.Graphics, rc, this.ClientRectangle.Width);
            DrawActiveCell(e.Graphics);
        }

        /// <summary>
        /// WANIGrid Size 변경 이벤트 발생 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WANIGrid_SizeChanged(object sender, EventArgs e)
        {
            rc = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            ActiveCell.Clear();
            InitializeScollBar();
            ReCalcScrollBars();
            Invalidate();
        }

        /// <summary>
        /// Client Size 변경 이벤트 발생 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WANIGrid_ClientSizeChanged(object sender, EventArgs e)
        {
            rc = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            ActiveCell.Clear();
            InitializeScollBar();
            ReCalcScrollBars();
            Invalidate();
        }

        /// <summary>
        /// Mouse Down Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WANIGrid_MouseDown(object sender, MouseEventArgs e)
        {
            //마우스 우측 버튼 클릭 시 Context 메뉴 제공
            if (e.Button == MouseButtons.Right)
            {
                //WANIGrid Header 영역이 선택되어졌을 경우에는 메뉴 제공하지 않음.
                if (e.Y < grid.TopHeaderHeight) return;

                ShowContextMenu(sender, e);
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
                SelectedRowsChangeColor(row);
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
                    SelectedRowsChangeColor(ActiveCell.Row);
                }
            }
        }

        /// <summary>
        /// Mouse Up Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Mouse Move Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// TextBox인 editBox의 TextChanged Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBox_TextChanged(object sender, EventArgs e)
        {
            if (rows.Count < 1)
            {
                editBox.Visible = false;
                return;
            }

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
        #endregion Event                        
    }
}
