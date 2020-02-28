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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magazine.AdminFolder
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        List<UserDto> users = new List<UserDto>();
        public Users()
        {
            InitializeComponent();
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ChangeUser createUser = new ChangeUser();
            //createUser.ShowDialog();
            // ChangeUser changeUser = new ChangeUser(ID);
            try
            {
                UserDto currentObject = (UserDto)phonesGrid.SelectedItem;
                ChangeME change = new ChangeME(currentObject.ID);
                change.ShowDialog();
                Update();
            }
            catch
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddUser add = new AddUser();
           
            add.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Update();
        }
        private void Update()
        {
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
                            users.AddRange(videogames.Where(x=>x.UsersType=="Seller"));
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            List<UserDto> tempusers = new List<UserDto>();
            foreach (var item in users)
            {
                if (item.ID.ToString().Contains(search.Text) || item.Login.Contains(search.Text) || item.Surname.Contains(search.Text) || item.UsersType.Contains(search.Text) || item.Name.Contains(search.Text))
                {
                    tempusers.Add(item);

                }
            }
            try
            {
                if (tempusers.Count != 0)
                {
                    phonesGrid.ItemsSource = null;
                    phonesGrid.ItemsSource = tempusers;
                }
                else
                {
                    MessageBox.Show("Нічого не знайдено");
                }
            }
            catch
            {

            }
            search.Text = "";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            UserDto currentObject = (UserDto)phonesGrid.SelectedItem;
            ChangeME change = new ChangeME(currentObject.ID);
            change.ShowDialog();
            Update();
        }
    }
}
