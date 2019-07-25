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
    public class DefaultHeader : Header
    {        
        public DefaultHeader()
        {
            this.ColumnId = "";
            this.Title = "";
            this.Width = 90;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = HorizontalAlignment.Left;
            this.Visible = true;
        }

        public DefaultHeader(string fieldName, string title, int width, HorizontalAlignment headAlign, HorizontalAlignment txtAlign, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = headAlign;
            this.TextAlign = txtAlign;
            this.Visible = visible;
        }
        public DefaultHeader(string fieldName, string title, int width, HorizontalAlignment txtAlign, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = txtAlign;
            this.Visible = visible;
        }

        public DefaultHeader(string fieldName, string title, HorizontalAlignment txtAlign, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = 100;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = txtAlign;
            this.Visible = visible;
        }

        public DefaultHeader(string fieldName, string title, int width, bool visible)
        {
            this.ColumnId = fieldName;
            this.Title = title;
            this.Width = width;
            this.HeadAlign = HorizontalAlignment.Center;
            this.TextAlign = HorizontalAlignment.Center;
            this.Visible = visible;
        }

        public DefaultHeader(string fieldName, string title, int width, HorizontalAlignment txtAlign)
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
