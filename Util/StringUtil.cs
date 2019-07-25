using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
