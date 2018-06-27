using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public class customDataGridView:DataGridView
    {
        public customDataGridView()
        {
            DoubleBuffered = true;
        }
    }
}
