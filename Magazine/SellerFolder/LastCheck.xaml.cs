using Magazine.ModelsDto;
using MyPrint;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace Magazine.SellerFolder
{
    /// <summary>
    /// Interaction logic for LastCheck.xaml
    /// </summary>
    public partial class LastCheck : Window
    {
        List<ProductInCheckDto> products = null;
        CheckDto check = null;
        double? SumM=0;
        public LastCheck(List<ProductInCheckDto> products, CheckDto check)
        {
            InitializeComponent();
            HelperSeller.IsSuccessfull = false;
            this.products = products;
            this.check = check;
            foreach (var item in products)
            {
                if(item.IsNumurable==true)
                {
                    SumM += item.Price * item.Count;
                  
                }
                else
                {
                    SumM += item.Price * item.Massa;
                 
                }
            }
            SumM= Math.Round(SumM.Value, 1);
            Getted.Focus();
            Getted.Text = SumM.ToString();
            CheckNUM.Content = "ЧЕК № " + check.ID;
            Sum.Content = "Сумма до сплати: " + SumM.Value+"грн" ;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Getted_TextChanged(object sender, TextChangedEventArgs e)
        {
            double i;
            bool bNum = double.TryParse(Getted.Text, out i);
            if (bNum)
            {
                Returned.Text = (i - SumM).ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double i;
            bool bNum = double.TryParse(Getted.Text, out i);
            if (bNum)
            {
                if ((i - SumM.Value)>=0)
                {
                    check.SumPrice = SumM;
                    check.DateCloseOfCheck = DateTime.Now;
                    //check.Products = products;
                    if(typee.Text== "Особистими")
                    {
                        check.TypeOfPay = "Особистими";
                        

                    }
                    else if (typee.Text == "Карткой")
                    {
                        check.TypeOfPay = "Карткой";
                        
                    }
                    else
                    {
                        check.TypeOfPay = "В кредит";
                        
                    }
                   
                    if (check.TypeOfPay != "В кредит")
                    {
                        string WEBSERVICE_URL2 = StaticHelper.URL + @"api/Apii/EndCheck";

                        var webRequest2 = System.Net.WebRequest.Create(WEBSERVICE_URL2);
                        if (webRequest2 != null)
                        {
                            webRequest2.Method = "POST";
                            webRequest2.Timeout = 12000;
                            webRequest2.ContentType = "application/json";
                            webRequest2.Headers.Add("Safety", "Safety");
                            using (var streamWriter2 = new StreamWriter(webRequest2.GetRequestStream()))
                            {
                                List<string> kist = new List<string>();
                                var json = JsonConvert.SerializeObject(products);
                                kist.Add(json);
                                var json2 = JsonConvert.SerializeObject(check);
                                kist.Add(json2);
                                streamWriter2.Write(JsonConvert.SerializeObject(kist));
                            }
                            //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                            using (System.IO.Stream s2 = webRequest2.GetResponse().GetResponseStream())
                            {
                                using (System.IO.StreamReader sr2 = new System.IO.StreamReader(s2))
                                {
                                    try
                                    {
                                        HelperSeller.IsSuccessfull = true;
                                        if (Printt.Text == "Ні")
                                        {

                                        }
                                        else
                                        {
                                            Print();
                                        }
                                        Close();
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(Borg.Text!="")
                        {
                            if(Borg.Text.Count()<5)
                            {
                                System.Windows.MessageBox.Show("Заповніть повну інформацію, призвіще і ініціали!");
                            }
                            else
                            {
                                string WEBSERVICE_URL2 = StaticHelper.URL + @"api/Apii/EndCheckCredit";

                                var webRequest2 = System.Net.WebRequest.Create(WEBSERVICE_URL2);
                                if (webRequest2 != null)
                                {
                                    webRequest2.Method = "POST";
                                    webRequest2.Timeout = 12000;
                                    webRequest2.ContentType = "application/json";
                                    webRequest2.Headers.Add("Safety", "Safety");
                                    using (var streamWriter2 = new StreamWriter(webRequest2.GetRequestStream()))
                                    {
                                        List<string> kist = new List<string>();
                                        var json = JsonConvert.SerializeObject(products);
                                        kist.Add(json);
                                        var json2 = JsonConvert.SerializeObject(check);
                                        kist.Add(json2);
                                        var json3 = JsonConvert.SerializeObject(Borg.Text);
                                        kist.Add(json3);
                                        streamWriter2.Write(JsonConvert.SerializeObject(kist));
                                    }
                                    //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                                    using (System.IO.Stream s2 = webRequest2.GetResponse().GetResponseStream())
                                    {
                                        using (System.IO.StreamReader sr2 = new System.IO.StreamReader(s2))
                                        {
                                            try
                                            {
                                                HelperSeller.IsSuccessfull = true;
                                                if (Printt.Text == "Ні")
                                                {

                                                }
                                                else
                                                {
                                                    Print();
                                                }
                                                Close();
                                            }
                                            catch
                                            {
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("ЗАПОВНІТЬ БОРЖНИКА!");
                        }
                        //TODO CREDIT
                        
                    }
                    

                }
                else
                {
                    System.Windows.MessageBox.Show("Помилка. Здача не можу бути від'ємною");
                }
            }
            
        }
        private void Print()
        {
            var rpt = new Temp();
            rpt.Session = new Dictionary<string, object>();
            List<OrderItem> items = new List<OrderItem>();
            bool isnull = false;
            ProductInCheckDto productwithnull = new ProductInCheckDto();
            foreach (var item in products)
            {
                if (item.IDOfProduct == 0)
                {
                    isnull = true;
                    productwithnull = item;
                }
                else
                {
                    OrderItem order = new OrderItem();
                    order.Name = item.Name;
                    order.Price = item.Price.Value.ToString();
                    if (item.IsNumurable == true)
                    {
                        order.Count = item.Count.Value.ToString();
                    }
                    else
                    {
                        order.Count = item.Massa.Value.ToString();
                    }
                    items.Add(order);
                }
            }
            if (isnull)
            {
                items.Add(new OrderItem() { Name = "Інші продукти", Count = "-", Price = productwithnull.Massa.Value.ToString() });
            }
            else
            {

            }
            rpt.Session["Model"] = new ReportModel
            {
                Sum = (Math.Round(check.SumPrice.Value, 1)).ToString()+" грн",
                Date = DateTime.Now, OrderItems = items
                
                
            };
           
            rpt.Initialize();
          (webBrowser22.Child as System.Windows.Forms.WebBrowser).DocumentText= rpt.TransformText();
            (webBrowser22.Child as System.Windows.Forms.WebBrowser).DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted); ;
      }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as System.Windows.Forms.WebBrowser).Print();
        }

        private void Typee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(typee.Text== "В кредит")
            {
                Borg.IsEnabled = true;
            }
            else
            {
                Borg.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

            //typee.SelectedIndex = typee.Items.IndexOf("foreach (ComboBoxItem cbi in someComboBox.Items)
        }
    }
}
