using System;
using System.Collections.Generic;
using System.Globalization;
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
namespace WANI_Grid.Util
{
    public static class DateUtil
    {
        /// <summary>
        /// 특정일자의 주차 구하기
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static int GetWeekNumber(int year, int month, int day, DayOfWeek dayOfWeek)
        {
            DateTime calculationDate = new DateTime(year, month, day); //주차를 구할 일자
            DateTime standardDate = new DateTime(year, 1, 1);   //기준일
            Calendar calendarCalc = CultureInfo.CurrentCulture.Calendar;
            int weekNumber = calendarCalc.GetWeekOfYear(calculationDate, CalendarWeekRule.FirstDay, dayOfWeek)
                 - calendarCalc.GetWeekOfYear(standardDate, CalendarWeekRule.FirstDay, dayOfWeek) + 1;
            return weekNumber;
        }

        /// <summary>
        /// 특정일자의 주차 구하기
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static int GetWeekNumber(DateTime dt, DayOfWeek dayOfWeek)
        {
            int year = dt.Year;
            DateTime calculationDate = dt; //주차를 구할 일자
            DateTime standardDate = new DateTime(year, 1, 1);   //기준일
            Calendar calendarCalc = CultureInfo.CurrentCulture.Calendar;
            int weekNumber = calendarCalc.GetWeekOfYear(calculationDate, CalendarWeekRule.FirstDay, dayOfWeek)
                 - calendarCalc.GetWeekOfYear(standardDate, CalendarWeekRule.FirstDay, dayOfWeek) + 1;
            return weekNumber;
        }
    }
}
