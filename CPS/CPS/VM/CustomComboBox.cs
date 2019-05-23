using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CPS.VM
{
    class CustomComboBox : ComboBox
    {
        public CustomComboBox(string[] items)
        {
            this.Margin = new System.Windows.Thickness(10, 0, 10, 0);
            foreach (var item in items) this.Items.Add(item);

            this.SelectedIndex = 0;
        }
    }
}
