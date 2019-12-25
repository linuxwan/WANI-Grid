using System;
using System.Collections.Generic;
using System.Drawing;
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
namespace WANI_Grid.Grid.Symbol
{
    public class Symbol : ISymbol
    {
        #region 변수선언
        private string symbolKey;   //Symbol의 Key값
        private string rowKey;  //Symbol이 위치하는 RowKey값
        private DateTime startDate; //Symbol 시작일자
        private DateTime finishDate;    //Symbol 종료일자
        private int duration;   //시작일자와 종료일자 사이의 일수
        #endregion 변수선언

        #region Property
        /// <summary>
        /// Symbol를 구분하는 Key 값
        /// </summary>
        public string SymbolKey
        {
            get { return symbolKey; }
            set { symbolKey = value; }
        }
        /// <summary>
        /// Symbol의 Row 위치를 결정하는 WANIGrid의 RowKey
        /// </summary>
        public string RowKey
        {
            get { return rowKey; }
            set { rowKey = value; }
        }
        /// <summary>
        /// Symbol의 시작 위치를 의미하는 시작일자
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        /// <summary>
        /// Symbol의 마지막 위치를 의미하는 종료일자
        /// </summary>
        public DateTime FinishDate
        {
            get { return finishDate; }
            set { finishDate = value; }
        }
        #endregion Property

        /// <summary>
        /// Symbol 그리기
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="point"></param>
        /// <param name="rect"></param>
        public void Draw(Graphics graphic, Point point, Rectangle rect)
        {
            throw new NotImplementedException();
        }
    }
}
