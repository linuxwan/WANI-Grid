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
    
    [ToolboxBitmap(typeof(WANIGrid), "WANIGrid")]
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
        public Cell ActiveCell = new Cell(-1, -1);
        private int ActiveCell_ActiveRow = 0;
        private int ActiveCell_ActvieCol = -1;
        private bool readOnly = false;
        private TextBox editBox = null;
        private GridState gridState = GridState.NONE;
        private SolidBrush blackBrush = new SolidBrush(Color.Black);
        private SolidBrush colFixBrush = new SolidBrush(SystemColors.ControlLight); //고정컬럼 배경색
        private bool vSpliteLineMouseDown = false;  //컬럼 경계선 상에서 마우스 좌측버튼이 눌러졌는지를 저장하기 위한 변수
        private int resizeCol = 0;  //사이즈 변경이 발생한 컬럼을 저장하기 위한 변수
        private Point lastMousePoint = new Point(0, 0); //마우스 좌측 버튼을 누른 상태에서 마지막 이동 Point를 저장하기 위한 변수
        private bool isShowContextMenu = true;    //ContextMenu 제공 여부
        private int colFixed = 0;   //고정 컬럼 개수(Column Header형태)
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
            set {                
                dataSource = value;
                SetDataTable(dataSource);
            }
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

        /// <summary>
        /// 마우스 우측 버튼 클릭 시 ContextMenu 제공 여부
        /// </summary>
        public bool IsShowContextMenu
        {
            get { return isShowContextMenu; }
            set { isShowContextMenu = value; }
        }

        public List<int> SelectedRows
        {
            get { return selectedRows; }            
        }

        public List<int> SelectedCols
        {
            get { return selectedCols; }
        }

        /// <summary>
        /// 고정 컬럼 개수
        /// </summary>
        public int ColFixed
        {
            get { return colFixed; }
            set { colFixed = value; }
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

            rightClickMenu.MenuItems[0].Click += new EventHandler(OnMenu_BeforeInsertRow_Click);
            rightClickMenu.MenuItems[1].Click += new EventHandler(OnMenu_AfterInsertRow_Click);
            rightClickMenu.MenuItems[2].Click += new EventHandler(OnMenu_AppenRow_Click);
            rightClickMenu.MenuItems[3].Click += new EventHandler(OnMenu_DeleteRow_Click);
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
        #endregion 초기화       
    }
}