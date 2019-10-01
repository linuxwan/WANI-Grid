using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public abstract class Header
    {
        #region 변수
        private int index;  //Index
        private string columnId;    //컬럼 ID
        private string title;   //컬럼 타이틀
        private int left;   //컬럼 시작위치
        private int width;  //컬럼 폭
        private HorizontalAlignment headAlign;  //컬럼 타이틀 정렬위치
        private HorizontalAlignment textAlign;  //컬럼 내용 정렬위치
        private bool visible = true;    //컬럼 Visible 여부
        private bool editable = true;   //컬럼 편집여부
        private bool isDate = false;    //Calendar를 그리기 위한 날짜 정보인지 여부
        #endregion 변수

        #region Property
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public string ColumnId {
            get { return columnId; }
            set { columnId = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public int Left
        {
            get { return left; }
            set { left = value; }
        }
        /// <summary>
        /// 컬럼의 폭을 제어하는 속성. 최소 제한 폭은 5
        /// </summary>
        public int Width
        {
            get { return width; }
            set
            {
                if (value < 5)
                    width = 5;
                else
                    width = value;
                OnWidthResized();
            }
        }
        public HorizontalAlignment HeadAlign
        {
            get { return headAlign; }
            set { headAlign = value; }
        }
        public HorizontalAlignment TextAlign {
            get { return textAlign; }
            set { textAlign = value; }
        }
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public bool Editable
        {
            get { return editable; }
            set { editable = value; }
        }

        public bool IsDate
        {
            get { return isDate; }
            set { isDate = value; }
        }
        #endregion Property

        #region Event
        public event EventHandler WidthResized;
        private void OnWidthResized()
        {
            WidthResized?.Invoke(this, new EventArgs());
        }
        #endregion
    }
}

