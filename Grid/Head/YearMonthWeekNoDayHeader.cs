using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WANI_Grid.Util;
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
    public class YearMonthWeekNoDayHeader : Header
    {
        private DateTime dateTime;  //헤더(Column)가 Calendar 날짜를 저장하고 있는 영역일 경우 DateTime 값을 가지게 됨
        private int weekNumber; //DateTime 값을 가지고 있으면 현 날짜의 주차 정보를 저장하기 위한 변수
        private string yearMonth;   //DateTime 값을 가지게 되면 년/월 값을 저장하기 위한 변수
        private bool isHoliday = false; //휴일 유무에 대한 bool 값을 저장
        private string pattern = ".";   //년월 구분자 (예. "." : 2019.09, "-" : 2019-09)

        #region Property
        /// <summary>
        /// DateTime 값을 반환. 외부에서 지정 불가.
        /// </summary>
        public DateTime GetDateTime
        {
            get { return dateTime; }
        }

        /// <summary>
        /// DateTime에 해당하는 주차 정보를 반환. 외부에서 지정 불가.
        /// </summary>
        public int WeekNumber
        {
            get { return weekNumber; }
        }

        /// <summary>
        /// DateTime에 해당하는 년월 정보를 반환. 외부에서 지정 불가
        /// </summary>
        public string YearMonth
        {
            get { return yearMonth; }
        }

        /// <summary>
        /// 휴일 여부를 반환
        /// </summary>
        public bool IsHoliday
        {
            get
            {
                //토,일을 휴일로 지정. 지정 휴일일 경우 isHoliday를 true로 설정하면 됨.
                if (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday) isHoliday = true;
                return isHoliday;
            }
            set { isHoliday = value; }
        }

        /// <summary>
        /// 년과 월사이의 구분 기호를 가져오거나 설정한다.
        /// </summary>
        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }
        #endregion Property

        #region 생성자
        public YearMonthWeekNoDayHeader()
        {
            this.ColumnId = "";
            this.Title = "";
            this.Width = 10;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = HorizontalAlignment.Left;
            this.Visible = true;            
        }
        #endregion 생성자
        
        public YearMonthWeekNoDayHeader(DateTime dt, DayOfWeek dayOfWeek)
        {
            this.ColumnId = dt.ToString("yyyyMMdd");
            int day = dt.Day;
            string strDay = "";
            if (day < 10) strDay = "0" + day;
            else strDay = day.ToString();
            this.Title = strDay;
            this.Width = 10;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = HorizontalAlignment.Center;
            this.Visible = true;
            this.dateTime = dt;
            this.weekNumber = DateUtil.GetWeekNumber(dt, dayOfWeek);
            string strMonth = "";
            if (dt.Month < 10) strMonth = "0" + dt.Month;
            else strMonth = dt.Month.ToString();
            this.yearMonth = dt.Year + pattern + strMonth;
            this.Editable = false;
            this.IsDate = true;
        }        

        public YearMonthWeekNoDayHeader(string fieldName, string title, int width, HorizontalAlignment headAlign, HorizontalAlignment txtAlign, bool visible, bool editable)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = headAlign;
            this.TextAlign = txtAlign;
            this.Visible = visible;
            this.Editable = editable;
        }

        public YearMonthWeekNoDayHeader(string fieldName, string title, int width, HorizontalAlignment headAlign, HorizontalAlignment txtAlign, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = headAlign;
            this.TextAlign = txtAlign;
            this.Visible = visible;
        }
        public YearMonthWeekNoDayHeader(string fieldName, string title, int width, HorizontalAlignment txtAlign, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = txtAlign;
            this.Visible = visible;
        }

        public YearMonthWeekNoDayHeader(string fieldName, string title, HorizontalAlignment txtAlign, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = 100;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = txtAlign;
            this.Visible = visible;
        }

        public YearMonthWeekNoDayHeader(string fieldName, string title, int width, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = HorizontalAlignment.Center;
            this.Visible = visible;
        }

        public YearMonthWeekNoDayHeader(string fieldName, string title, int width, HorizontalAlignment txtAlign)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = txtAlign;
            this.Visible = true;
        }        
    }
}
