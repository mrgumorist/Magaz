using Magazine.AdminFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Magazine.SellerFolder
{
    /// <summary>
    /// Interaction logic for ChangeNotAddedProduct.xaml
    /// </summary>
    public partial class ChangeNotAddedProduct : Window
    {
        public ChangeNotAddedProduct(string sum)
        {
            InitializeComponent();
            Sum.Text = sum;
            Sum.Focus();
            Sum.CaretIndex = Sum.Text.Length;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double num = -1;
            if (Double.TryParse(Sum.Text, out num))
            {
                if (num > 0)
                {
                    Helper.issuccessful = true;
                    Helper.mass = num;
                    Close();
                }
                else
                {
                    MessageBox.Show("Помилка");
                }
            }
            else
            {
                MessageBox.Show("Помилка");

            }
        }

        private void Sum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click(sender, e);
            }
        }
    }
}
