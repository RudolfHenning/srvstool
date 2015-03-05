using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SrvsTool.Controls
{
    public class TextBoxEx : TextBox
    {
        public event MethodInvoker EnterKeyPressed;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (EnterKeyPressed != null)
                {
                    EnterKeyPressed();
                    e.Handled = true;
                }
            base.OnKeyPress(e);
        }
    }
}
