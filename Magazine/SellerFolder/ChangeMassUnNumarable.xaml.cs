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
    /// Interaction logic for ChangeMassUnNumarable.xaml
    /// </summary>
    public partial class ChangeMassUnNumarable : Window
    {
        ProductInCheckDto product1 = null;
        double maxWeigth=0;
        //ProductDto product = null;
        public ChangeMassUnNumarable(ProductInCheckDto product1, double maxweight)
        {
            Helper.count = -1;
            Helper.issuccessful = false;
            Helper.mass = -1;
            this.product1 = product1;
            InitializeComponent();
            maxWeigth = maxweight;
            //GetMaxWeigthById
            ForText.Content = maxweight.ToString() + " kg";
            Tovar.Content = "Товар: " + product1.Name;
            Num.Text = product1.Massa.ToString();
            InitializeComponent();
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
                    if (mass > 0 && mass <= maxWeigth)
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
    }
}
