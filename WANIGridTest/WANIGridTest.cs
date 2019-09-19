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
            builder.AddHeader(new DefaultHeader("Col02", "Column 02", 80, HorizontalAlignment.Center, HorizontalAlignment.Right, false));
            builder.AddHeader(new DefaultHeader("Col03", "Column 03", 90, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col04", "Column 04", 100, HorizontalAlignment.Center, HorizontalAlignment.Center, true));
            builder.AddHeader(new DefaultHeader("Col05", "Column 05", 110, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col06", "Column 06", 120, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col07", "Column 07", 110, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col08", "Column 08", 100, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col09", "Column 09", 80, HorizontalAlignment.Center, HorizontalAlignment.Center, true));
            builder.AddHeader(new DefaultHeader("Col10", "Column 10", 90, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col11", "Column 11", 90, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.InitializeHeader();
            this.waniGrid.SetHeader(builder);
            //ContextMenu 숨기기 - 아래의 주석을 풀면 됨
            //this.waniGrid.IsShowContextMenu = false;
            //고정 컬럼 설정
            this.waniGrid.ColFixed = 0;            
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

        /// <summary>
        /// DataTable에서 데이터 가져오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetDataTable_Click(object sender, EventArgs e)
        {
            DataTable dataTable = makeDataTable();
            this.waniGrid.DataSource = dataTable;
        }

        private DataTable makeDataTable()
        {
            DataTable dt = new DataTable("DataTable");

            DataColumn col1 = new DataColumn();
            col1.DataType = System.Type.GetType("System.String");
            col1.ColumnName = "Col01";
            dt.Columns.Add(col1);

            DataColumn col2 = new DataColumn();
            col2.DataType = System.Type.GetType("System.Int32");
            col2.ColumnName = "Col02";
            dt.Columns.Add(col2);

            DataColumn col3 = new DataColumn();
            col3.DataType = System.Type.GetType("System.String");
            col3.ColumnName = "Col03";
            dt.Columns.Add(col3);

            DataColumn col4 = new DataColumn();
            col4.DataType = System.Type.GetType("System.Int32");
            col4.ColumnName = "Col04";
            dt.Columns.Add(col4);

            DataColumn col5 = new DataColumn();
            col5.DataType = System.Type.GetType("System.String");
            col5.ColumnName = "Col05";
            dt.Columns.Add(col5);

            DataColumn col6 = new DataColumn();
            col6.DataType = System.Type.GetType("System.Int32");
            col6.ColumnName = "Col06";
            dt.Columns.Add(col6);

            DataColumn col7 = new DataColumn();
            col7.DataType = System.Type.GetType("System.String");
            col7.ColumnName = "Col07";
            dt.Columns.Add(col7);

            DataColumn col8 = new DataColumn();
            col8.DataType = System.Type.GetType("System.Int32");
            col8.ColumnName = "Col08";
            dt.Columns.Add(col8);

            DataColumn col9 = new DataColumn();
            col9.DataType = System.Type.GetType("System.String");
            col9.ColumnName = "Col09";
            dt.Columns.Add(col9);

            DataColumn col10 = new DataColumn();
            col10.DataType = System.Type.GetType("System.Int32");
            col10.ColumnName = "Col10";
            dt.Columns.Add(col10);

            DataColumn col11 = new DataColumn();
            col11.DataType = System.Type.GetType("System.String");
            col11.ColumnName = "Col11";
            dt.Columns.Add(col11);

            for (int i = 0; i < 100000; i++)
            {
                DataRow dr = dt.NewRow();

                for (int j = 1; j <= 11; j++)
                {
                    string colId = String.Empty;
                    if (j < 10)
                    {
                        colId += "Col0" + j;
                    }
                    else
                    {
                        colId = "Col" + j;
                    }

                    if (j % 2 == 1)
                    {
                        dr[colId] = "Test " + (i + 1);
                    }
                    else
                    {
                        dr[colId] = i + 1;
                    }
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
