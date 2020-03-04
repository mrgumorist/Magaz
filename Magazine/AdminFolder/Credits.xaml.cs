using Magazine.ModelsDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Credits.xaml
    /// </summary>
    public partial class Credits : Page
    {
        List<CreditDto> credits = new List<CreditDto>();
        public Credits()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Update
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetAllCredits";

            var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
            if (webRequest1 != null)
            {
                webRequest1.Method = "GET";
                webRequest1.Timeout = 12000;
                webRequest1.ContentType = "application/json";
                webRequest1.Headers.Add("Safety", "Safety");
                using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                    {
                        try
                        {
                            var jsonResponse1 = sr1.ReadToEnd();
                            List<CreditDto> videogames = JsonConvert.DeserializeObject<List<CreditDto>>(jsonResponse1);
                            credits.Clear();
                            credits.AddRange(videogames);
                            CreditsDrid.ItemsSource = credits;
                            //MessageBox.Show("Kek");
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
    }
}
