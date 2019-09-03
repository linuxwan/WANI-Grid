using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WANI_Grid.Grid.Head;

namespace WANIGridTest
{
    public partial class WANIGridTest : Form
    {
        public WANIGridTest()
        {
            InitializeComponent();
        }

        private void WANIGridTest_Load(object sender, EventArgs e)
        {
            HeaderBuilder builder = new HeaderBuilder(this.waniGrid.GridDisplayType);
            builder.AddHeader(new DefaultHeader("Col01", "Column 01", 80, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col02", "Column 02", 80, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col03", "Column 03", 90, HorizontalAlignment.Center, HorizontalAlignment.Left, false));
            builder.AddHeader(new DefaultHeader("Col04", "Column 04", 100, HorizontalAlignment.Center, HorizontalAlignment.Center, true));
            builder.AddHeader(new DefaultHeader("Col05", "Column 05", 110, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col06", "Column 06", 120, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col07", "Column 07", 110, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col08", "Column 08", 100, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col09", "Column 09", 80, HorizontalAlignment.Center, HorizontalAlignment.Center, true));
            builder.AddHeader(new DefaultHeader("Col10", "Column 10", 90, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.InitializeHeader();
            this.waniGrid.SetHeader(builder);
            //ContextMenu 숨기기 - 아래의 주석을 풀면 됨
            //this.waniGrid.IsShowContextMenu = false;
            //고정 컬럼 설정
            this.waniGrid.ColFixed = 2;
        }

        /// <summary>
        /// Context Menu 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnContextMenuShow_Click(object sender, EventArgs e)
        {
            this.waniGrid.IsShowContextMenu = true;
        }

        /// <summary>
        /// Context Menu 숨기기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnContextMenuHide_Click(object sender, EventArgs e)
        {
            this.waniGrid.IsShowContextMenu = false;
        }

        /// <summary>
        /// 이전 행(Row) 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBeforeInsertRow_Click(object sender, EventArgs e)
        {
            if (this.waniGrid.SelectedRows.Count < 1)
            {
                MessageBox.Show("행(Row)을 선택하셔야 합니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int crntRow = this.waniGrid.ActiveCell.Row;
            this.waniGrid.BeforeInsert(crntRow);
        }

        /// <summary>
        /// 다음 행(Row) 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAtferInsertRow_Click(object sender, EventArgs e)
        {
            if (this.waniGrid.SelectedRows.Count < 1)
            {
                MessageBox.Show("행(Row)을 선택하셔야 합니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int crntRow = this.waniGrid.ActiveCell.Row;
            this.waniGrid.AfterInsert(crntRow);
        }

        /// <summary>
        /// 행(Row) 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            this.waniGrid.AppendRow();
        }

        /// <summary>
        /// 행(Row) 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteRow_Click(object sender, EventArgs e)
        {
            if (this.waniGrid.SelectedRows.Count < 1)
            {
                MessageBox.Show("삭제할 행(Row)을 선택하셔야 합니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.waniGrid.SelectedRows.Count > 0)
            {
                for (int i = 0; i < this.waniGrid.SelectedRows.Count; i++)
                {
                    int crntRow = this.waniGrid.SelectedRows[i];
                    this.waniGrid.DeleteRow(crntRow);
                    this.waniGrid.RebuildSelectedRowsIndex(crntRow);
                }
                this.waniGrid.SelectedRows.Clear();
            }
        }
    }
}
