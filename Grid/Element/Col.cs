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
namespace WANI_Grid.Grid.Element
{
    /// <summary>
    /// 1개의 Row에 대한 Column들의 Text값을 가져오기 위한 클래스
    /// </summary>
    public class Col
    {
        #region 변수
        private List<Header> colHeaders;    //Column의 Header정보를 저장
        private DataRow row;    //Row의 Column정보를 저장하기 위한 DataRow
        private HorizontalAlignment alignment;  //Column의 Text 정렬 방법
        #endregion 변수

        #region Property
        /// <summary>
        /// Header에 정의된 Column 리스트 설정하거나 반환한다.
        /// </summary>
        public List<Header> ColHeaders
        {
            get { return this.colHeaders; }
            set { this.colHeaders = value; }
        }

        /// <summary>
        /// Column의 정렬방법
        /// </summary>
        public HorizontalAlignment Alignment
        {
            get { return this.alignment; }
        }

        /// <summary>
        /// Row의 Column정보를 저장하기 위한 DataRow
        /// </summary>
        public DataRow Row
        {
            get { return this.row; }
            set { this.row = value; }
        }      
        #endregion Property

        #region 생성자        
        /// <summary>
        /// 생성자 - 한개의 행에 대한 Column Header정보와 DataRow를 파라미터로 받음
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="row"></param>
        public Col(List<Header> headers, DataRow row)
        {
            this.colHeaders = headers;
            this.row = row;
        }        

        /// <summary>
        /// Column ID 값으로 DataRow내의 컬럼 값을 반환
        /// </summary>
        /// <param name="columnId"></param>
        /// <returns></returns>
        public string GetColText(string columnId)
        {
            string text = String.Empty;
            if (row[columnId] != null) text = row[columnId].ToString();

            return text;
        }

        /// <summary>
        /// Column Index에 해당하는 Column 값을 반환
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetColText(int index)
        {
            string text = String.Empty;
            int colNo = 0;
            foreach (Header head in colHeaders)
            {
                //Column Header에서 해당 Column을 Visible true일 경우에만 값을 반환 - 화면에 Display하기 위함
                if (head.Visible)
                {
                    if (head.Index == index)
                    {
                        text = row[head.ColumnId].ToString();
                        alignment = head.TextAlign;
                    }
                    colNo++;
                }
            }

            return text;
        }
        #endregion 생성자
    }
}
