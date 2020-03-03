using Magazine.ModelsDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magazine.AdminFolder
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            D1.SelectedDate = DateTime.Today;
            D2.SelectedDate = DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (D1.SelectedDate.Value.ToLongDateString() == D2.SelectedDate.Value.ToLongDateString()) {

                
            }
            else
            {
                
            }
            
               string WEBSERVICE_URL1 = StaticHelper.URL + $@"api/Apii/GetSalesByDate";

               var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
               if (webRequest1 != null)
               {
                   webRequest1.Method = "POST";
                   webRequest1.Timeout = 12000;
                   webRequest1.ContentType = "application/json";
                   webRequest1.Headers.Add("Safety", "Safety");
                   using (var streamWriter2 = new StreamWriter(webRequest1.GetRequestStream()))
                   {
                    DateDto date = new DateDto();
                    date.date = D1.SelectedDate.Value;
                       streamWriter2.Write(JsonConvert.SerializeObject(date));
                   }
                   try
                   {
                       using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                       {
                           using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                           {
                               try
                               {
                                   var jsonResponse1 = sr1.ReadToEnd();
                                List<CheckDto> checks = JsonConvert.DeserializeObject<List<CheckDto>>(jsonResponse1);
                                //MessageBox.Show(jsonResponse1);
                                ChecksGrid.ItemsSource = checks;
                                double sum = 0;
                                foreach (var item in checks)
                                {
                                    sum += item.SumPrice.Value;
                                }
                                MainReport.Content = "Загальна сума: " + sum+" грн!";
                               }
                               catch
                               {
                                   MessageBox.Show("Error");
                               }
                           }
                       }
                   }
                   catch
                   {
                       MessageBox.Show("Error");
                   }
                   }
       
        }
    }
}
