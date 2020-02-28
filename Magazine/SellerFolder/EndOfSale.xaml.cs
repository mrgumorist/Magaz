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
    /// Interaction logic for EndOfSale.xaml
    /// </summary>
    public partial class EndOfSale : Window
    {
        List<ProductInCheckDto> products = null;
        CheckDto check = null;
        public EndOfSale(List<ProductInCheckDto> products, CheckDto check)
        {
            InitializeComponent();
            HelperSeller.IsSuccessfull = false;
            this.products = products;
            this.check = check;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Close
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Close
        }
    }
}
