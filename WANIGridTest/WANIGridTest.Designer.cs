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
            this.chkFixedColEditable = new System.Windows.Forms.CheckBox();
            this.numUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.finishDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cboGridType = new System.Windows.Forms.ComboBox();
            this.btnGetDataTable = new System.Windows.Forms.Button();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnAtferInsertRow = new System.Windows.Forms.Button();
            this.btnBeforeInsertRow = new System.Windows.Forms.Button();
            this.btnContextMenuHide = new System.Windows.Forms.Button();
            this.btnContextMenuShow = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cboStartDayOfWeek = new System.Windows.Forms.ComboBox();
            this.waniGrid = new WANI_Grid.WANIGrid();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 100);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.cboStartDayOfWeek);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkFixedColEditable);
            this.groupBox1.Controls.Add(this.numUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.finishDatePicker);
            this.groupBox1.Controls.Add(this.startDatePicker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboGridType);
            this.groupBox1.Controls.Add(this.btnGetDataTable);
            this.groupBox1.Controls.Add(this.btnDeleteRow);
            this.groupBox1.Controls.Add(this.btnAddRow);
            this.groupBox1.Controls.Add(this.btnAtferInsertRow);
            this.groupBox1.Controls.Add(this.btnBeforeInsertRow);
            this.groupBox1.Controls.Add(this.btnContextMenuHide);
            this.groupBox1.Controls.Add(this.btnContextMenuShow);
            this.groupBox1.Location = new System.Drawing.Point(6, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 114);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // chkFixedColEditable
            // 
            this.chkFixedColEditable.AutoSize = true;
            this.chkFixedColEditable.Location = new System.Drawing.Point(350, 25);
            this.chkFixedColEditable.Name = "chkFixedColEditable";
            this.chkFixedColEditable.Size = new System.Drawing.Size(100, 16);
            this.chkFixedColEditable.TabIndex = 21;
            this.chkFixedColEditable.Text = "고정컬럼 수정";
            this.chkFixedColEditable.UseVisualStyleBackColor = true;
            this.chkFixedColEditable.CheckedChanged += new System.EventHandler(this.ChkFixedColEditable_CheckedChanged);
            // 
            // numUpDown
            // 
            this.numUpDown.Location = new System.Drawing.Point(299, 22);
            this.numUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.Size = new System.Drawing.Size(44, 21);
            this.numUpDown.TabIndex = 20;
            this.numUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "고정컬럼 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "종료일자 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "시작일자 :";
            // 
            // finishDatePicker
            // 
            this.finishDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.finishDatePicker.Location = new System.Drawing.Point(243, 45);
            this.finishDatePicker.Name = "finishDatePicker";
            this.finishDatePicker.Size = new System.Drawing.Size(100, 21);
            this.finishDatePicker.TabIndex = 16;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(71, 45);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(100, 21);
            this.startDatePicker.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "Grid 유형 :";
            // 
            // cboGridType
            // 
            this.cboGridType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGridType.FormattingEnabled = true;
            this.cboGridType.Location = new System.Drawing.Point(70, 22);
            this.cboGridType.Name = "cboGridType";
            this.cboGridType.Size = new System.Drawing.Size(144, 20);
            this.cboGridType.TabIndex = 13;
            this.cboGridType.SelectedIndexChanged += new System.EventHandler(this.CboGridType_SelectedIndexChanged);
            // 
            // btnGetDataTable
            // 
            this.btnGetDataTable.Location = new System.Drawing.Point(4, 71);
            this.btnGetDataTable.Name = "btnGetDataTable";
            this.btnGetDataTable.Size = new System.Drawing.Size(134, 23);
            this.btnGetDataTable.TabIndex = 12;
            this.btnGetDataTable.Text = "DataTable 가져오기";
            this.btnGetDataTable.UseVisualStyleBackColor = true;
            this.btnGetDataTable.Click += new System.EventHandler(this.BtnGetDataTable_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Location = new System.Drawing.Point(724, 45);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRow.TabIndex = 11;
            this.btnDeleteRow.Text = "행 삭제";
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            this.btnDeleteRow.Click += new System.EventHandler(this.BtnDeleteRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(724, 20);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 10;
            this.btnAddRow.Text = "행 추가";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.BtnAddRow_Click);
            // 
            // btnAtferInsertRow
            // 
            this.btnAtferInsertRow.Location = new System.Drawing.Point(638, 45);
            this.btnAtferInsertRow.Name = "btnAtferInsertRow";
            this.btnAtferInsertRow.Size = new System.Drawing.Size(83, 23);
            this.btnAtferInsertRow.TabIndex = 9;
            this.btnAtferInsertRow.Text = "다음 행 추가";
            this.btnAtferInsertRow.UseVisualStyleBackColor = true;
            this.btnAtferInsertRow.Click += new System.EventHandler(this.BtnAtferInsertRow_Click);
            // 
            // btnBeforeInsertRow
            // 
            this.btnBeforeInsertRow.Location = new System.Drawing.Point(638, 20);
            this.btnBeforeInsertRow.Name = "btnBeforeInsertRow";
            this.btnBeforeInsertRow.Size = new System.Drawing.Size(83, 23);
            this.btnBeforeInsertRow.TabIndex = 8;
            this.btnBeforeInsertRow.Text = "이전 행 추가";
            this.btnBeforeInsertRow.UseVisualStyleBackColor = true;
            this.btnBeforeInsertRow.Click += new System.EventHandler(this.BtnBeforeInsertRow_Click);
            // 
            // btnContextMenuHide
            // 
            this.btnContextMenuHide.Location = new System.Drawing.Point(501, 45);
            this.btnContextMenuHide.Name = "btnContextMenuHide";
            this.btnContextMenuHide.Size = new System.Drawing.Size(134, 23);
            this.btnContextMenuHide.TabIndex = 7;
            this.btnContextMenuHide.Text = "Context Menu 숨기기";
            this.btnContextMenuHide.UseVisualStyleBackColor = true;
            this.btnContextMenuHide.Click += new System.EventHandler(this.BtnContextMenuHide_Click);
            // 
            // btnContextMenuShow
            // 
            this.btnContextMenuShow.Location = new System.Drawing.Point(501, 20);
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
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel2.Size = new System.Drawing.Size(812, 423);
            this.panel2.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(349, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "시작요일 :";
            // 
            // cboStartDayOfWeek
            // 
            this.cboStartDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStartDayOfWeek.FormattingEnabled = true;
            this.cboStartDayOfWeek.Location = new System.Drawing.Point(416, 45);
            this.cboStartDayOfWeek.Name = "cboStartDayOfWeek";
            this.cboStartDayOfWeek.Size = new System.Drawing.Size(70, 20);
            this.cboStartDayOfWeek.TabIndex = 23;
            this.cboStartDayOfWeek.SelectedIndexChanged += new System.EventHandler(this.CboStartDayOfWeek_SelectedIndexChanged);
            // 
            // waniGrid
            // 
            this.waniGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waniGrid.ColFixed = 0;
            this.waniGrid.DataSource = null;
            this.waniGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waniGrid.FixedColEditable = true;
            grid1.DicMonthDay = null;
            grid1.DicWeekDay = null;
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
            this.waniGrid.Size = new System.Drawing.Size(802, 418);
            this.waniGrid.TabIndex = 1;
            // 
            // WANIGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 523);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "WANIGridTest";
            this.Text = "WANI Grid Test";
            this.Load += new System.EventHandler(this.WANIGridTest_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).EndInit();
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
        private System.Windows.Forms.Button btnGetDataTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGridType;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker finishDatePicker;
        private System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFixedColEditable;
        private System.Windows.Forms.ComboBox cboStartDayOfWeek;
        private System.Windows.Forms.Label label5;
    }
}

