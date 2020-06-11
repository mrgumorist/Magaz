using Magazine.AdminFolder;
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

namespace Magazine.SellerFolder
{
    /// <summary>
    /// Interaction logic for SetMassUnNumarable.xaml
    /// </summary>
    public partial class SetMassUnNumarable : Window
    {
        ProductDto product = null;
        public SetMassUnNumarable(ProductDto product)
        {
            Helper.count = -1;
            Helper.issuccessful = false;
            Helper.mass = -1;
           
            this.product = product;
            InitializeComponent();
            Tovar.Content = "Товар: " + product.Name;
            ForText.Content = product.Massa.ToString()+" kg";
            Num.CaretIndex = Num.Text.Length;
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
                    double mass = double.Parse(Num.Text);
                    if (mass > 0 && mass <= product.Massa)
                    {
                        Helper.issuccessful = true;
                        Helper.count = -1;
                        Helper.mass = mass;

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Помилка. Можливо число більше доступного");
                    }
                }
                else
                {
                    MessageBox.Show("Число не вірне. Приклад числа: 3,14");
                }
            }
        }
        public static bool IsNumeric(string Expression)
        {
            double price;
            if (Double.TryParse(Expression, out price))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click_1(sender, e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Num.Focus();
        }
    }
}
