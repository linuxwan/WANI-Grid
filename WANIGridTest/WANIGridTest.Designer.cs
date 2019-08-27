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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnAtferInsertRow = new System.Windows.Forms.Button();
            this.btnBeforeInsertRow = new System.Windows.Forms.Button();
            this.btnContextMenuHide = new System.Windows.Forms.Button();
            this.btnContextMenuShow = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.waniGrid = new WANI_Grid.WANIGrid();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 74);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteRow);
            this.groupBox1.Controls.Add(this.btnAddRow);
            this.groupBox1.Controls.Add(this.btnAtferInsertRow);
            this.groupBox1.Controls.Add(this.btnBeforeInsertRow);
            this.groupBox1.Controls.Add(this.btnContextMenuHide);
            this.groupBox1.Controls.Add(this.btnContextMenuShow);
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 69);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Location = new System.Drawing.Point(231, 38);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRow.TabIndex = 11;
            this.btnDeleteRow.Text = "행 삭제";
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            this.btnDeleteRow.Click += new System.EventHandler(this.BtnDeleteRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(231, 13);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 10;
            this.btnAddRow.Text = "행 추가";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.BtnAddRow_Click);
            // 
            // btnAtferInsertRow
            // 
            this.btnAtferInsertRow.Location = new System.Drawing.Point(145, 38);
            this.btnAtferInsertRow.Name = "btnAtferInsertRow";
            this.btnAtferInsertRow.Size = new System.Drawing.Size(83, 23);
            this.btnAtferInsertRow.TabIndex = 9;
            this.btnAtferInsertRow.Text = "다음 행 추가";
            this.btnAtferInsertRow.UseVisualStyleBackColor = true;
            this.btnAtferInsertRow.Click += new System.EventHandler(this.BtnAtferInsertRow_Click);
            // 
            // btnBeforeInsertRow
            // 
            this.btnBeforeInsertRow.Location = new System.Drawing.Point(145, 13);
            this.btnBeforeInsertRow.Name = "btnBeforeInsertRow";
            this.btnBeforeInsertRow.Size = new System.Drawing.Size(83, 23);
            this.btnBeforeInsertRow.TabIndex = 8;
            this.btnBeforeInsertRow.Text = "이전 행 추가";
            this.btnBeforeInsertRow.UseVisualStyleBackColor = true;
            this.btnBeforeInsertRow.Click += new System.EventHandler(this.BtnBeforeInsertRow_Click);
            // 
            // btnContextMenuHide
            // 
            this.btnContextMenuHide.Location = new System.Drawing.Point(8, 38);
            this.btnContextMenuHide.Name = "btnContextMenuHide";
            this.btnContextMenuHide.Size = new System.Drawing.Size(134, 23);
            this.btnContextMenuHide.TabIndex = 7;
            this.btnContextMenuHide.Text = "Context Menu 숨기기";
            this.btnContextMenuHide.UseVisualStyleBackColor = true;
            this.btnContextMenuHide.Click += new System.EventHandler(this.BtnContextMenuHide_Click);
            // 
            // btnContextMenuShow
            // 
            this.btnContextMenuShow.Location = new System.Drawing.Point(8, 13);
            this.btnContextMenuShow.Name = "btnContextMenuShow";
            this.btnContextMenuShow.Size = new System.Drawing.Size(134, 23);
            this.btnContextMenuShow.TabIndex = 6;
            this.btnContextMenuShow.Text = "Context Menu 보이기";
            this.btnContextMenuShow.UseVisualStyleBackColor = true;
            this.btnContextMenuShow.Click += new System.EventHandler(this.BtnContextMenuShow_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.waniGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 74);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel2.Size = new System.Drawing.Size(613, 298);
            this.panel2.TabIndex = 2;
            // 
            // waniGrid
            // 
            this.waniGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.waniGrid.IsShowContextMenu = true;
            this.waniGrid.LeftHeaderWidth = 22;
            this.waniGrid.Location = new System.Drawing.Point(5, 0);
            this.waniGrid.Name = "waniGrid";
            this.waniGrid.Size = new System.Drawing.Size(603, 293);
            this.waniGrid.TabIndex = 1;
            // 
            // WANIGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 372);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "WANIGridTest";
            this.Text = "WANI Grid Test";
            this.Load += new System.EventHandler(this.WANIGridTest_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private WANI_Grid.WANIGrid waniGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteRow;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnAtferInsertRow;
        private System.Windows.Forms.Button btnBeforeInsertRow;
        private System.Windows.Forms.Button btnContextMenuHide;
        private System.Windows.Forms.Button btnContextMenuShow;
    }
}

