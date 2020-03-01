using Magazine.ModelsDto;
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

namespace Magazine.AdminFolder
{
    /// <summary>
    /// Interaction logic for SetCountNumarable.xaml
    /// </summary>
    public partial class SetCountNumarable : Window
    {
        
        ProductDto product = null;
        public SetCountNumarable(ProductDto product)
        {
            Helper.count = -1;
            Helper.issuccessful = false;
            Helper.mass = -1;
            this.product = product;
            InitializeComponent();
            ForText.Content = product.Count;
            Tovar.Content = "Товар: " + product.Name;
            Num.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.issuccessful = false;
            Helper.count = -1;
            Helper.mass = -1;
            Close();
            //Cancel
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Num.Text != "")
            {
                if (IsNumeric(Num.Text))
                {
                    int count = int.Parse(Num.Text);
                    if (count > 0 && count <= product.Count)
                    {
                        Helper.issuccessful = true;
                        Helper.count = count;
                        Helper.mass = -1;

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Помилка. Можливо число більше доступного");
                    }
                }
            }
        }
        public static bool IsNumeric(object Expression)
        {
            int retNum;

            bool isNum = int.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private void Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click_1(sender, e);
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Num.Focus();
        }
    }
}
