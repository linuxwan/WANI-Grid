using System;
using System.Collections.Generic;
using System.Drawing;
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
namespace WANI_Grid.Util
{
    public static class StringUtil
    {
        public static string TruncatedString(string s, int width, int offset, Graphics g, Font headerFont)
        {
            string strPrint = "";
            int swid;
            int i;

            SizeF strSize;

            try
            {
                strSize = g.MeasureString(s, headerFont);
                swid = ((int)strSize.Width);
                i = s.Length;

                for (i = s.Length; i > 0 && swid > width - offset; i--)
                {
                    strSize = g.MeasureString(s.Substring(0, i), headerFont);
                    swid = ((int)strSize.Width);
                }

                if (i < s.Length)
                {
                    if (i - 2 <= 0)
                    {
                        if (i == 1)
                        {
                            strPrint = ".";
                        }
                        else if (i == 2)
                        {
                            strPrint = s.Substring(0, 1);
                        }
                        else
                        {
                            strPrint = "";
                        }
                    }
                    strPrint = s.Substring(0, i - 3) + "...";
                }
                else
                    strPrint = s.Substring(0, i);
            }
            catch
            {
            }
            return strPrint;
        }

        public static StringAlignment GetStringAlignment(StringFormat sf, HorizontalAlignment align)
        {
            if (align == HorizontalAlignment.Left)
                sf.Alignment = StringAlignment.Near;
            else if (align == HorizontalAlignment.Center)
                sf.Alignment = StringAlignment.Center;
            else if (align == HorizontalAlignment.Right)
                sf.Alignment = StringAlignment.Far;

            return sf.Alignment;
        }
    }
}
