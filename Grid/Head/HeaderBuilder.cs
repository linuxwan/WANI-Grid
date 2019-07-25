using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class HeaderBuilder
    {
        private GridType gridType = GridType.DefaultType;
        private HeaderGenerator headGen;

        #region Property
        public GridType GridDisplayType
        {
            get { return gridType; }
            set { gridType = value; }
        }

        public HeaderGenerator HeaderGen
        {
            get { return headGen; }
        }
        #endregion Property

        #region 생성자
        public HeaderBuilder() { }
        public HeaderBuilder(GridType grdType)
        {
            this.gridType = grdType;
            if (gridType == GridType.DefaultType)
            {
                if (headGen == null) headGen = new DefaultHeaderGenerator();
            }
        }
        #endregion 생성자

        #region Method                
        public void AddHeader(Header header)
        {
            if (gridType == GridType.DefaultType)
            {
                if (headGen == null) headGen = new DefaultHeaderGenerator();
                headGen.AddHeaders(header);
            }
        }

        public List<Header> GetHeaders()
        {
            return headGen.GetHeaders();
        }        
        #endregion Method

        #region 초기화
        /// <summary>
        /// 설정한 Grid Header를 초기화한다. 컬럼 헤더의 폭과 왼쪽 시작 좌표를 설정
        /// </summary>
        public void InitializeHeader()
        {            
            //헤더 설정이 되어져 있지 않을 경우
            if (headGen == null || headGen.GetHeaders().Count < 1) return;

            int index = 0;
            int left = 0;

            foreach (Header header in headGen.GetHeaders())
            {
                header.Index = index;
                header.Left = left;
                left += header.Width;
                index++;
            }
        }
        #endregion 초기화
    }
}
