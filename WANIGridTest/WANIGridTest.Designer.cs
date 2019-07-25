namespace WANIGridTest
{
    partial class WANIGridTest
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            WANI_Grid.Grid.Grid grid1 = new WANI_Grid.Grid.Grid();
            this.waniGrid = new WANI_Grid.WANIGrid();
            this.SuspendLayout();
            // 
            // waniGrid
            // 
            this.waniGrid.DataSource = null;
            this.waniGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            grid1.FirstVisibleCol = 0;
            grid1.FirstVisibleRow = 0;
            grid1.GridDisplayType = WANI_Grid.Grid.GridType.DefaultType;
            grid1.HeaderGen = null;
            grid1.LastVisibleCol = 0;
            grid1.LastVisibleRow = 0;
            grid1.LeftHeaderWidth = 22;
            grid1.TopHeaderHeight = 20;
            this.waniGrid.Grid = grid1;
            this.waniGrid.GridDisplayType = WANI_Grid.Grid.GridType.DefaultType;
            this.waniGrid.HeaderFont = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.waniGrid.LeftHeaderWidth = 22;
            this.waniGrid.Location = new System.Drawing.Point(0, 0);
            this.waniGrid.Name = "waniGrid";
            this.waniGrid.Size = new System.Drawing.Size(613, 372);
            this.waniGrid.TabIndex = 0;
            // 
            // WANIGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 372);
            this.Controls.Add(this.waniGrid);
            this.Name = "WANIGridTest";
            this.Text = "WANI Grid Test";
            this.Load += new System.EventHandler(this.WANIGridTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WANI_Grid.WANIGrid waniGrid;
    }
}

