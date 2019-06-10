using System;
using System.Windows;
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

        public CustomTextBox(bool isReadOnly)
        {
            this.Margin = new System.Windows.Thickness(10, 0, 10, 0);
            this.Text = "0,0";
            this.IsReadOnly = isReadOnly;
        }

        public T GetValue()
        {
            try
            {
                T result = (T)Convert.ChangeType(this.Text.Replace('.', ','), typeof(T));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return (T)Convert.ChangeType(this.Text.Replace('.', ','), typeof(T));
        }
    }
}
