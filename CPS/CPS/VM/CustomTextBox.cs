using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CPS.VM
{
    class CustomTextBox<T> : TextBox
    {
        public CustomTextBox(double value = 0)
        {
            this.Margin = new System.Windows.Thickness(10, 0, 10, 0);
            this.Text = value.ToString();
        }

        public T GetValue()
        {
            T example = (T)Convert.ChangeType(this.Text, typeof(T));

            Console.WriteLine(example.ToString());

            return (T)Convert.ChangeType(this.Text, typeof(T));
        }
    }
}
