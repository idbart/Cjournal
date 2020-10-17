using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cjournal_Desktop.Controls
{
    /// <summary>
    /// Interaction logic for NumberBox.xaml
    /// </summary>
    public partial class NumberBox : UserControl
    {
        public NumberBox()
        {
            InitializeComponent();
        }

        // a method for getting the input value
        public int getInputValue()
        {
            int value;
            return int.TryParse(input.Text, out value) ? value : 0;
        }
        // a method for setting the input value
        public void setInputValue(int value)
        {
            input.Text = value.ToString();
        }

        // for when the user inputs something new into the text box
        private void input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !isOnlyNumeric(e.Text);
        } 
        
        // for determining if a string contains only numbers
        private bool isOnlyNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+");

            return !regex.IsMatch(text);
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            editNumber(1);
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            editNumber(-1);
        }

        // add the given value to the input number
        private void editNumber(int number)
        {
            setInputValue(getInputValue() + number);
        }
    }
}
