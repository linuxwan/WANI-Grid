using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
