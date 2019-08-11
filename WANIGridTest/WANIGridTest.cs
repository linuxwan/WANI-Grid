using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WANI_Grid.Grid.Head;

namespace WANIGridTest
{
    public partial class WANIGridTest : Form
    {
        public WANIGridTest()
        {
            InitializeComponent();
        }

        private void WANIGridTest_Load(object sender, EventArgs e)
        {
            HeaderBuilder builder = new HeaderBuilder(this.waniGrid.GridDisplayType);
            builder.AddHeader(new DefaultHeader("Col01", "Column 01", 80, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col02", "Column 02", 80, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col03", "Column 03", 90, HorizontalAlignment.Center, HorizontalAlignment.Left, false));
            builder.AddHeader(new DefaultHeader("Col04", "Column 04", 100, HorizontalAlignment.Center, HorizontalAlignment.Center, true));
            builder.AddHeader(new DefaultHeader("Col05", "Column 05", 110, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col06", "Column 06", 120, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col07", "Column 07", 110, HorizontalAlignment.Center, HorizontalAlignment.Right, true));
            builder.AddHeader(new DefaultHeader("Col08", "Column 08", 100, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.AddHeader(new DefaultHeader("Col09", "Column 09", 80, HorizontalAlignment.Center, HorizontalAlignment.Center, true));
            builder.AddHeader(new DefaultHeader("Col10", "Column 10", 90, HorizontalAlignment.Center, HorizontalAlignment.Left, true));
            builder.InitializeHeader();
            this.waniGrid.SetHeader(builder);
            //this.waniGrid.SelectedColor = Color.LightBlue;
        }
    }
}
