using Magazine.ModelsDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for CreditChange.xaml
    /// </summary>
    public partial class CreditChange : Window
    {
        CreditDto credit;
        public CreditChange( CreditDto credit)
        {
            InitializeComponent();
            Sum.Text = credit.Sum.ToString();
            this.credit = credit;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double sum;
            if (Double.TryParse(Sum.Text, out sum))
            {
                if(sum>0&&sum <= credit.Sum)
                {
                    credit.Sum -= sum;
                    string WEBSERVICE_URL1 = StaticHelper.URL + $@"api/Apii/UpdateCredit";

                    var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                    if (webRequest1 != null)
                    {
                        webRequest1.Method = "POST";
                        webRequest1.Timeout = 12000;
                        webRequest1.ContentType = "application/json";
                        webRequest1.Headers.Add("Safety", "Safety");
                        using (var streamWriter2 = new StreamWriter(webRequest1.GetRequestStream()))
                        {

                              var json = JsonConvert.SerializeObject(credit);
                            streamWriter2.Write(json);
                        }
                        //webRequest1.Headers.Add("SpecialCode", Code.Text);
                        using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                        {
                            using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            {
                                try
                                {
                                    var jsonResponse1 = sr1.ReadToEnd();
                                    MessageBox.Show("Готово! Оновіть будь ласка.");
                                    Close();



                                }
                                catch
                                {
                                    MessageBox.Show("Error");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
