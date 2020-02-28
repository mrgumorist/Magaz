using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magazine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StaticHelper.ReadFromFile();
        }
        private void Kek(object sender, RoutedEventArgs e)
        {
        }
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Pass.Password;
            string WEBSERVICE_URL = StaticHelper.URL + @"api/Apii/login";

            var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Login", login);
                webRequest.Headers.Add("Safety", "Safety");
                webRequest.Headers.Add("Password", password);
                //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        try
                        {
                            var jsonResponse = sr.ReadToEnd();
                            if (int.Parse(jsonResponse) != -1)
                            {
                                string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetTypeOfAccount";

                                var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                                if (webRequest1 != null)
                                {
                                    webRequest1.Method = "GET";
                                    webRequest1.Timeout = 12000;
                                    webRequest1.ContentType = "application/json";
                                    webRequest1.Headers.Add("ID", jsonResponse);
                                    //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                                    using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                                    {
                                        using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                                        {
                                            try
                                            {
                                                var jsonResponse1 = sr1.ReadToEnd();
                                                if(jsonResponse1==@"""Creator""")
                                                {
                                                    this.Hide();
                                                    
                                                    CreatorFolder.CreatorPanel panel = new CreatorFolder.CreatorPanel();
                                                    panel.Closed += Panel_Closed;
                                                    panel.ShowDialog();
                                                   // this.Show();
                                                    //if(Kek+=panel.Closing)
                                                    // Show();
                                                }
                                                else if(jsonResponse1 == @"""SuperAdmin""")
                                                {
                                                    this.Hide();
                                                    SuperAdminFolder.SuperAdminPanel panel = new SuperAdminFolder.SuperAdminPanel(int.Parse(jsonResponse));
                                                    panel.Closed += Panel_Closed;
                                                    panel.ShowDialog();
                                                   // this.Show();
                                                }
                                                else if (jsonResponse1 == @"""Admin""")
                                                {
                                                   
                                                    AdminFolder.AdminPanel panel = new AdminFolder.AdminPanel(int.Parse(jsonResponse));
                                                    panel.Closed += Panel_Closed;
                                                    this.Hide();
                                                    panel.ShowDialog();
                                                    //this.Show();
                                                }
                                                else 
                                                {
                                                  
                                                    SellerFolder.SellerPanel panel = new SellerFolder.SellerPanel(int.Parse(jsonResponse));
                                                    panel.Closed += Panel_Closed;
                                                    this.Hide();
                                                    panel.ShowDialog();

                                                    //this.Show();
                                                }
                                                Login.Clear();
                                                Pass.Clear();
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
                                MessageBox.Show("Невдала спроба входу");
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Невдала спроба входу");
                        }
                        //MessageBox.Show("Все чудово.");
                        //this.Close();
                    }
                }
            }
        }

        private void Panel_Closed(object sender, EventArgs e)
        {
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для зміни пароля - зверніться до адміністратора!");
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Pass.Focus();
            }
        }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click(sender, e);
            }
        }
    }
}
