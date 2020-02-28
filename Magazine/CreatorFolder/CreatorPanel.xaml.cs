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

namespace Magazine.CreatorFolder
{
    /// <summary>
    /// Interaction logic for CreatorPanel.xaml
    /// </summary>
    public partial class CreatorPanel : Window
    {
        List<UserDto> users = new List<UserDto>();
        public CreatorPanel()
        {
            InitializeComponent();
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
        private void Update()
        {
            //List<string> videogames = JsonConvert.DeserializeObject<List<string>>(json);
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetUsers";

            var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
            if (webRequest1 != null)
            {
                webRequest1.Method = "GET";
                webRequest1.Timeout = 12000;
                webRequest1.ContentType = "application/json";
                webRequest1.Headers.Add("Safety", "Safety");
                //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                    {
                        try
                        {
                            var jsonResponse1 = sr1.ReadToEnd();
                            List<UserDto> videogames = JsonConvert.DeserializeObject<List<UserDto>>(jsonResponse1);
                            users.Clear();
                            users.AddRange(videogames);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            phonesGrid.ItemsSource = null;
       phonesGrid.ItemsSource = users;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserDto currentObject = (UserDto)phonesGrid.SelectedItem;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевнені що бажаєте видалити "+ currentObject.Name, "Підтвердження дії", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/DeleteUser";

                var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                if (webRequest1 != null)
                {
                    webRequest1.Method = "GET";
                    webRequest1.Timeout = 12000;
                    webRequest1.ContentType = "application/json";
                    webRequest1.Headers.Add("Safety", "Safety");
                    webRequest1.Headers.Add("ID", currentObject.ID.ToString());
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
                                MessageBox.Show("Успішне видалення");
                                Update();
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

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hide();
            CreateUser createUser = new CreateUser();
            createUser.ShowDialog();
            Update();
            Show();
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UserDto currentObject = (UserDto)phonesGrid.SelectedItem;

            Hide();
            ChangeUser change = new ChangeUser(currentObject.ID);
            change.ShowDialog();
            Update();
            Show();
        }
    }
}
