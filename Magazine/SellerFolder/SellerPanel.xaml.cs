using Magazine.AdminFolder;
using Magazine.ModelsDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for SellerPanel.xaml
    /// </summary>
    public partial class SellerPanel : Window
    {
        int CurrentCheckId;
        int ID;
        CheckDto CurrentCheck = new CheckDto();
        List<ProductInCheckDto> NumarableList = new List<ProductInCheckDto>();
        List<ProductInCheckDto> UnNumarableList = new List<ProductInCheckDto>();
        public SellerPanel(int ID)
        {
            InitializeComponent();
            CurrentCheckId = -1;
            this.ID = ID;
            LoadCheck();
            LoadInfo();
            //LoadCheck();
        }
        private void LoadCheck()
        {
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetIdOfCheck";

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
                            //MessageBox.Show(jsonResponse1);
                            jsonResponse1 = jsonResponse1.Replace(@"""", "");
                            CurrentCheckId = int.Parse(jsonResponse1);
                            CheckNum.Content = CurrentCheckId.ToString();
                            CurrentCheck.ID = CurrentCheckId;
                        }
                        catch
                        {

                        }

                    }
                }
            }
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
                            NameUser.Text = user.Name;
                            SurnameUser.Text = user.Surname;

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
            ChangePassword changePassword = new ChangePassword(ID);
            this.Hide();
            changePassword.ShowDialog();
            ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeInfo ChangeInfo = new ChangeInfo(ID);
            this.Hide();
            ChangeInfo.ShowDialog();
            LoadInfo();
            ShowDialog();
        }

        private void Searching_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                search();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            search();
        }
        private void Clear()
        {
            ProductsGrid.ItemsSource = null;
            Searching.Text = "";
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Cancel search
            Clear();
        }
        
        private void search()
        {
            if (Searching.Text != "")
            {
                if (Comboo.Text == "По штрих коду")
                {
                    //GetBySpecialCode
                    string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetBySpecialCode";
                    var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                    if (webRequest1 != null)
                    {
                        webRequest1.Method = "Post";
                        webRequest1.Timeout = 12000;
                        webRequest1.ContentType = "application/json";
                        //webRequest1.Headers.Add("Querry", Searching.Text);
                        webRequest1.Headers.Add("Safety", "Safety");
                        webRequest1.Headers.Add("ID", ID.ToString());
                        using (var streamWriter2 = new StreamWriter(webRequest1.GetRequestStream()))
                        {

                            var json = JsonConvert.SerializeObject(Searching.Text);
                            streamWriter2.Write(json);
                        }
                        using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                        {
                            using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            {
                                try
                                {
                                    var jsonResponse1 = sr1.ReadToEnd();
                                    if (jsonResponse1 != @"0")
                                    {
                                        List<ProductDto> productDtos = JsonConvert.DeserializeObject<List<ProductDto>>(jsonResponse1);
                                        if (productDtos.Count != 1)
                                        {
                                            ProductsGrid.ItemsSource = productDtos;
                                        }
                                        else
                                        {
                                            NextStage(productDtos[0]);
                                            Clear();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("По даному коду нічого не найдено");
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
                else if (Comboo.Text == "За іменем")
                {
                    //GetBySpecialCode
                    string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetByName";
                    var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                    if (webRequest1 != null)
                    {
                        webRequest1.Method = "Post";
                        webRequest1.Timeout = 12000;
                        webRequest1.ContentType = "application/json";
                      //  webRequest1.Headers.Add("Querry", Searching.Text);
                        webRequest1.Headers.Add("Safety", "Safety");
                        webRequest1.Headers.Add("ID", ID.ToString());
                        using (var streamWriter2 = new StreamWriter(webRequest1.GetRequestStream()))
                        {

                            var json = JsonConvert.SerializeObject(Searching.Text);
                            streamWriter2.Write(json);
                        }
                        using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                        {
                            using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            {
                                try
                                {
                                    var jsonResponse1 = sr1.ReadToEnd();
                                    if (jsonResponse1 != @"0")
                                    {
                                        List<ProductDto> productDtos = JsonConvert.DeserializeObject<List<ProductDto>>(jsonResponse1);
                                        if (productDtos.Count != 1)
                                        {
                                            ProductsGrid.ItemsSource = productDtos;
                                        }
                                        else
                                        {
                                            NextStage(productDtos[0]);
                                            Clear();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("За даним ім'ям нічого не найдено");
                                    }
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

        private void ProductsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        //This is the code which helps to show the data when the row is double clicked.
                        DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        ProductDto dr = (ProductDto)dgr.Item;
                        if (dr.IsNumurable == true)
                        {
                            if(dr.Count==0)
                            {
                                MessageBox.Show("Кількість позицій даного товару 0");
                            }
                            else
                            {
                                NextStage(dr);
                                Clear();
                            }
                          
                        }
                        else
                        {
                            if (dr.Massa == 0)
                            {
                                MessageBox.Show("Масса даного товару 0");
                            }
                            else
                            {
                                NextStage(dr);
                                Clear();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void NextStage(ProductDto product)
        {
            bool IsExist = false;
            if (NumarableList.Count != 0)
            {
                foreach (var item in NumarableList)
                {
                    if (item.IDOfProduct == product.ID)
                    {
                        IsExist = true;
                        break;
                    }
                }
            }
            if (UnNumarableList.Count != 0)
            {
                foreach (var item in UnNumarableList)
                {
                    if (item.IDOfProduct == product.ID)
                    {
                        IsExist = true;
                        break;
                    }
                }
            }
            if (IsExist == false)
            {
                if(product.IsNumurable==true)
                {
                    
                        ProductInCheckDto productInCheck = new ProductInCheckDto();
                        productInCheck.IDOfProduct = product.ID;
                        productInCheck.IsNumurable = product.IsNumurable;
                        productInCheck.Name = product.Name;
                        productInCheck.SpecialCode = product.SpecialCode;
                        productInCheck.Price = product.Price;
                        productInCheck.Count = 1;
                        productInCheck.Check = CurrentCheck;
                        NumarableList.Add(productInCheck);
                        UpdateChecks();
                    //}
                }
                else
                {
                    SetMassUnNumarable set = new SetMassUnNumarable(product);
                    set.ShowDialog();
                    //MessageBox.Show(Helper.count.ToString());
                    if (Helper.issuccessful == true)
                    {
                        ProductInCheckDto productInCheck = new ProductInCheckDto();
                        productInCheck.IDOfProduct = product.ID;
                        productInCheck.IsNumurable = product.IsNumurable;
                        productInCheck.Name = product.Name;
                        productInCheck.SpecialCode = product.SpecialCode;
                        productInCheck.Price = product.Price;
                        productInCheck.Massa = Helper.mass;
                        productInCheck.Check = CurrentCheck;
                        UnNumarableList.Add(productInCheck);
                        UpdateChecks();
                    }
                }
            }
            else
            {
                
                if (product.IsNumurable == true)
                {
                    var ToChange = NumarableList.First(x => x.SpecialCode == product.SpecialCode);
                    string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetMaxCountById";
                    var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                    if (webRequest1 != null)
                    {
                        webRequest1.Method = "GET";
                        webRequest1.Timeout = 12000;
                        webRequest1.ContentType = "application/json";
                        // webRequest1.Headers.Add("Querry", Searching.Text);
                        webRequest1.Headers.Add("Safety", "Safety");
                        webRequest1.Headers.Add("ID", ToChange.IDOfProduct.ToString());
                        using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                        {
                            using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            {
                                try
                                {
                                    var jsonResponse1 = sr1.ReadToEnd();
                                    jsonResponse1 = jsonResponse1.Replace(@"""", "");
                                    int response111 = int.Parse(jsonResponse1);
                                    //MessageBox.Show(response.ToString());
                                    //ChangeCountNumarable update = new ChangeCountNumarable(ToChange, response111);
                                    //update.ShowDialog();
                                    //if (Helper.issuccessful == true)
                                    //{
                                    //    NumarableList.First(x => x.ID == ToChange.ID).Count = Helper.count;
                                    //    UpdateChecks();
                                    //}
                                    if (response111 >= NumarableList.First(x => x.IDOfProduct == ToChange.IDOfProduct).Count + 1)
                                    {
                                        NumarableList.First(x => x.IDOfProduct == ToChange.IDOfProduct).Count += 1;
                                        UpdateChecks();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Всі товари данного коду продані");
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.Message);
                                }
                            }
                        }
                    }
                    }
                else
                {
                    MessageBox.Show("Данний продукт уже є в чеку. Доповнюємо.");
                    var ToChange = UnNumarableList.First(x => x.IDOfProduct == product.ID);
                    string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetMaxWeigthById";
                    var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                    if (webRequest1 != null)
                    {
                        webRequest1.Method = "GET";
                        webRequest1.Timeout = 12000;
                        webRequest1.ContentType = "application/json";
                        // webRequest1.Headers.Add("Querry", Searching.Text);
                        webRequest1.Headers.Add("Safety", "Safety");
                        webRequest1.Headers.Add("ID", ToChange.IDOfProduct.ToString());
                        using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                        {
                            using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            {
                                try
                                {
                                    var jsonResponse1 = sr1.ReadToEnd();
                                    jsonResponse1 = jsonResponse1.Replace(".", ",");
                                    double response = double.Parse(jsonResponse1);
                                    ChangeMassUnNumarable update = new ChangeMassUnNumarable(ToChange, response);
                                    update.ShowDialog();
                                    if (Helper.issuccessful == true)
                                    {
                                        UnNumarableList.First(x => x.IDOfProduct == ToChange.IDOfProduct).Massa = Helper.mass;
                                        UpdateChecks();
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
                //Доповнення
            }
            //CurrentCheck.Products.Add();
        }
        private void UpdateChecks()
        {
            Numarable.ItemsSource = null;
            UnNumarable.ItemsSource = null;

            Numarable.ItemsSource = NumarableList;
            UnNumarable.ItemsSource = UnNumarableList;
            CountSum();
            UnNumarable.UpdateLayout();
            Numarable.UpdateLayout();
        }
        private void CountSum()
        {
            double sum = 0;
            foreach (var item in NumarableList)
            {
                sum += double.Parse((item.Count * item.Price).ToString());
            }
            foreach (var item in UnNumarableList)
            {
                sum += double.Parse((item.Massa * item.Price).ToString());
            }
            Sum.Content = sum.ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (sender != null)
            {
                try { 
                var menuItem = (MenuItem)sender;
                var contextMenu = (ContextMenu)menuItem.Parent;
                var item = (DataGrid)contextMenu.PlacementTarget;
                var ToChange = (ProductInCheckDto)item.SelectedCells[0].Item;
                    string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetMaxWeigthById";
                    var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                    if (webRequest1 != null)
                    {
                        webRequest1.Method = "GET";
                        webRequest1.Timeout = 12000;
                        webRequest1.ContentType = "application/json";
                       // webRequest1.Headers.Add("Querry", Searching.Text);
                        webRequest1.Headers.Add("Safety", "Safety");
                        webRequest1.Headers.Add("ID", ToChange.IDOfProduct.ToString());
                        using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                        {
                            using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            {
                                try
                                {
                                    var jsonResponse1 = sr1.ReadToEnd();
                                    jsonResponse1=jsonResponse1.Replace(".", ",");
                                    double response = double.Parse(jsonResponse1);
                                    ChangeMassUnNumarable update = new ChangeMassUnNumarable(ToChange, response);
                                    update.ShowDialog();
                                    if (Helper.issuccessful == true)
                                    {
                                        UnNumarableList.First(x => x.ID == ToChange.ID).Massa = Helper.mass;
                                        UpdateChecks();
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                   
                }
                catch
                {

                }
                //MessageBox.Show(toDeleteFromBindedList.Name);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                try
                {
                    var menuItem = (MenuItem)sender;
                    var contextMenu = (ContextMenu)menuItem.Parent;
                    var item = (DataGrid)contextMenu.PlacementTarget;
                    var ToDelete = (ProductInCheckDto)item.SelectedCells[0].Item;
                    MessageBoxResult res = MessageBox.Show("Ви точно впевнені що бажаєте видалити даний товар зі списку?", "Підтвердження", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        UnNumarableList.Remove(ToDelete);
                        MessageBox.Show("Успішно");
                        UpdateChecks();
                    }
                    else
                    {
                        MessageBox.Show("Скасовано");
                    }
                }
                catch
                {

                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Clear all
            NumarableList.Clear();
            UnNumarableList.Clear();
            UpdateChecks();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                try
                {
                    var menuItem = (MenuItem)sender;
                    var contextMenu = (ContextMenu)menuItem.Parent;
                    var item = (DataGrid)contextMenu.PlacementTarget;
                    var ToChange = (ProductInCheckDto)item.SelectedCells[0].Item;
                    string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetMaxCountById";
                    var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                    if (webRequest1 != null)
                    {
                        webRequest1.Method = "GET";
                        webRequest1.Timeout = 12000;
                        webRequest1.ContentType = "application/json";
                        // webRequest1.Headers.Add("Querry", Searching.Text);
                        webRequest1.Headers.Add("Safety", "Safety");
                        webRequest1.Headers.Add("ID", ToChange.IDOfProduct.ToString());
                        using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                        {
                            using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            {
                                try
                                {
                                    var jsonResponse1 = sr1.ReadToEnd();
                                    jsonResponse1=jsonResponse1.Replace(@"""", "");
                                    int response111 = int.Parse(jsonResponse1);
                                    //MessageBox.Show(response.ToString());
                                    ChangeCountNumarable update = new ChangeCountNumarable(ToChange, response111);
                                    update.ShowDialog();
                                    if (Helper.issuccessful == true)
                                    {
                                        NumarableList.First(x => x.ID == ToChange.ID).Count = Helper.count;
                                        UpdateChecks();
                                    }
                                }
                                catch(Exception ee)
                                {
                                    MessageBox.Show(ee.Message);
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                try { 
                var menuItem = (MenuItem)sender;
                var contextMenu = (ContextMenu)menuItem.Parent;
                var item = (DataGrid)contextMenu.PlacementTarget;
                var ToDelete = (ProductInCheckDto)item.SelectedCells[0].Item;
                MessageBoxResult res = MessageBox.Show("Ви точно впевнені що бажаєте видалити даний товар зі списку?", "Підтвердження", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    NumarableList.Remove(ToDelete);
                    MessageBox.Show("Успішно");
                    UpdateChecks();
                }
                else
                {
                    MessageBox.Show("Скасовано");
                }
                }
                catch
                {

                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            List<ProductInCheckDto> productss = new List<ProductInCheckDto>();
            productss.AddRange(UnNumarableList);
            productss.AddRange(NumarableList);
            if (productss.Count == 0)
            {
                MessageBox.Show("Помилка. Чек пустий.");
            }
            else
            {
                //EndOfSale end = new EndOfSale(productss, CurrentCheck);
                //end.ShowDialog();
                LastCheck last = new LastCheck(productss, CurrentCheck);
                last.ShowDialog();
                if(HelperSeller.IsSuccessfull)
                {
                    LoadCheck();
                    NumarableList.Clear();
                    UnNumarableList.Clear();
                    UpdateChecks();
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
           
        }
    }
}
