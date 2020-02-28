using Magazine.ModelsDto;
using Newtonsoft.Json;
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
    /// Interaction logic for ChangeInfo.xaml
    /// </summary>
    public partial class ChangeInfo : Window
    {
        int ID;
        public ChangeInfo(int ID)
        {
            InitializeComponent();
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetUserByID";
            this.ID = ID;
            var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
            if (webRequest1 != null)
            {
                webRequest1.Method = "GET";
                webRequest1.Timeout = 12000;
                webRequest1.ContentType = "application/json";
                webRequest1.Headers.Add("Safety", "Safety");
                webRequest1.Headers.Add("ID", ID.ToString());
                //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                    {
                        try
                        {
                            var jsonResponse1 = sr1.ReadToEnd();
                            UserDto user = JsonConvert.DeserializeObject<UserDto>(jsonResponse1);
                            Namee.Text = user.Name;
                            Surnamee.Text = user.Surname;

                            // MessageBox.Show("Успішно");
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/ChangeNameAndSurname";

            var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
            if (webRequest1 != null)
            {
                webRequest1.Method = "GET";
                webRequest1.Timeout = 12000;
                webRequest1.ContentType = "application/json";
                webRequest1.Headers.Add("Safety", "Safety");
                webRequest1.Headers.Add("ID", ID.ToString());
                webRequest1.Headers.Add("Name", Namee.Text);
                webRequest1.Headers.Add("Surname", Surnamee.Text);
                //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                    {
                        try
                        {
                            var jsonResponse1 = sr1.ReadToEnd();
                            //List<UserDto> videogames = JsonConvert.DeserializeObject<List<UserDto>>(jsonResponse1);
                            //users.Clear();
                            //users.AddRange(videogames);
                            //  MessageBox.Show(jsonResponse1);
                            MessageBox.Show("Вдало змінено");
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
}
