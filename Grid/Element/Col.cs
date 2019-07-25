using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WANI_Grid.Grid.Head;
/// <summary>
/// 이 소스는 LGPL(GNU Lesser General Public Licence)를 따릅니다.
/// 이 컨트롤을 사용하는 데에는 제한이 없으나, 해당 소스를 변경하거나 개선했을 경우에는 LGPL에 의해 소스를 제공해야 합니다.
/// 이 소스는 누구나 자유롭게 사용할 수 있고, 배포할 수 있습니다.
/// GitHub : https://github.com/linuxwan/WANI-Grid
/// Blog : https://uniworks.tistory.com
/// 작성자 : Park Chungwan
/// </summary>
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
