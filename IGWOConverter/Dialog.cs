using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace IGWOConverter
{
    public class Dialog
    {
        public Dialog()
        {
            Form converterDialog = new Form();
            converterDialog.Text = "OBJ to Instant Gameworks Object (IGWO) Converter";
            converterDialog.ShowDialog();
        }
    }
}
