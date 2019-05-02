using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CPS.ViewModel
{
    class CustomTextBox<T> : TextBox
    {
        public CustomTextBox()
        {
            this.Margin = new System.Windows.Thickness(10, 0, 10, 0);
        }

        public T GetValue()
        {
            return (T)Convert.ChangeType(this.Text, typeof(T));
        }
    }
}
