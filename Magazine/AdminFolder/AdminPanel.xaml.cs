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

namespace Magazine.AdminFolder
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        int ID;
        public AdminPanel(int ID)
        {
            this.ID = ID;
            InitializeComponent();
            LoadInfo();
        }
        private void LoadInfo()
        {
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetUserByID";
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
                            name.Content = user.Name;
                            surname.Content = user.Surname;
                            // MessageBox.Show("Успішно");
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frame1.Source = new Uri("Users.xaml", UriKind.RelativeOrAbsolute);
            frame2.Source = new Uri("Products.xaml", UriKind.RelativeOrAbsolute);
            frame3.Source = new Uri("Reports.xaml", UriKind.RelativeOrAbsolute);
            //frame4.Source = new Uri("Accounts.xaml", UriKind.RelativeOrAbsolute);
            frame7.Source = new Uri("Credits.xaml", UriKind.RelativeOrAbsolute);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Change password
            Hide();
            ChangeYourPass yourPass = new ChangeYourPass(ID);
            
            yourPass.ShowDialog();
            Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frame1.Visibility = Visibility.Visible;
            frame2.Visibility = Visibility.Hidden;
            frame3.Visibility = Visibility.Hidden;
            frame4.Visibility = Visibility.Hidden;
            frame5.Visibility = Visibility.Hidden;
            frame6.Visibility = Visibility.Hidden;
            frame7.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            frame1.Visibility = Visibility.Hidden;
            frame2.Visibility = Visibility.Visible;
            frame3.Visibility = Visibility.Hidden;
            frame4.Visibility = Visibility.Hidden;
            frame5.Visibility = Visibility.Hidden;
            frame6.Visibility = Visibility.Hidden;
            frame7.Visibility = Visibility.Hidden;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            frame1.Visibility = Visibility.Hidden;
            frame2.Visibility = Visibility.Hidden;
            frame3.Visibility = Visibility.Visible;
            frame4.Visibility = Visibility.Hidden;
            frame5.Visibility = Visibility.Hidden;
            frame6.Visibility = Visibility.Hidden;
            frame7.Visibility = Visibility.Hidden;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            frame1.Visibility = Visibility.Hidden;
            frame2.Visibility = Visibility.Hidden;
            frame3.Visibility = Visibility.Hidden;
            frame4.Visibility = Visibility.Visible;
            frame5.Visibility = Visibility.Hidden;
            frame6.Visibility = Visibility.Hidden;
            frame7.Visibility = Visibility.Hidden;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Change your information
            ChangeME changeUser = new ChangeME(ID);
            changeUser.ShowDialog();
            LoadInfo();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            frame1.Visibility = Visibility.Hidden;
            frame2.Visibility = Visibility.Hidden;
            frame3.Visibility = Visibility.Hidden;
            frame4.Visibility = Visibility.Hidden;
            frame5.Visibility = Visibility.Hidden;
            frame6.Visibility = Visibility.Visible;
            frame7.Visibility = Visibility.Hidden;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            frame1.Visibility = Visibility.Hidden;
            frame2.Visibility = Visibility.Hidden;
            frame3.Visibility = Visibility.Hidden;
            frame4.Visibility = Visibility.Hidden;
            frame5.Visibility = Visibility.Hidden;
            frame6.Visibility = Visibility.Hidden;
            frame7.Visibility = Visibility.Visible;
        }
    }
}
