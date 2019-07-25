using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WANI_Grid.Grid.Head;

namespace WANI_Grid.Grid.Element
{
    public class Col
    {
        #region 변수
        private Header colHeader;
        private string text;
        #endregion 변수

        #region Property
        /// <summary>
        /// Header에 정의된 정보가 바로 Column에 대한 정보
        /// </summary>
        public Header ColHeader
        {
            get { return this.colHeader; }
            set { this.colHeader = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public int Index
        {
            get { return this.colHeader.Index; }
        }
        #endregion Property

        #region 생성자
        public Col() { }

        public Col(Header header)
        {
            this.colHeader = header;
        }
        #endregion 생성자
    }
}
