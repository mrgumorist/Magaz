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

namespace Magazine.SuperAdminFolder
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/AddUser";

            var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
            if (webRequest1 != null)
            {
                webRequest1.Method = "POST";
                webRequest1.Timeout = 12000;
                webRequest1.ContentType = "application/json";
                webRequest1.Headers.Add("Safety", "Safety");
                using (var streamWriter = new StreamWriter(webRequest1.GetRequestStream()))
                {
                    UserDto user = new UserDto() { LastLogin = DateTime.Now, Name = d1.Text, Surname = d2.Text, Login = d3.Text, Password = d4.Password, ID = 0 };
                    string UsersType1 = d5.Text;
                    if (UsersType1 == "Адмін")
                    {
                        UsersType1 = "1";
                    }
                    if (UsersType1 == "Продавець")
                    {
                        UsersType1 = "3";
                    }
                    user.UsersType = UsersType1;
                    var json = JsonConvert.SerializeObject(user);
                    streamWriter.Write(json);
                }
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
                            MessageBox.Show("Додано!");
                            this.Close();
                        }
                        catch
                        {

                        }
                    }
                }
            }
        
    }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
