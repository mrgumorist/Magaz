using Magazine.AdminFolder;
using Magazine.ModelsDto;
using Magazine.Services;
using MyPrint;
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
        List<ProductInCheckDto> preCheckprod = null;
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
            if (CheckInternetConnection.IsConnectedToInternet() == true)
            {
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
            else
            {
                MessageBox.Show("Відсутнє з'єднання! Спробуйте відновити підключення.");
                Close();
            }
        }
        private void LoadInfo()
        {
            if (CheckInternetConnection.IsConnectedToInternet() == true)
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
            else
            {
                MessageBox.Show("Відсутнє з'єднання! Спробуйте відновити підключення.");
                Close();
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
                    if (Searching.Text != "0")
                    {
                        string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetBySpecialCode";
                        if (CheckInternetConnection.IsConnectedToInternet() == true)
                        {
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
                        else
                        {
                            MessageBox.Show("Помилка. Відсутнє з'єднання.");
                        }

                    }
                    else
                    {
                        bool founded = false;
                        foreach (var item in UnNumarableList)
                        {
                            if (item.IDOfProduct == 0)
                            {
                                founded = true;
                                break;
                            }
                        }
                        if (founded == true)
                        {
                            ChangeNotAddedProduct change = new ChangeNotAddedProduct(UnNumarableList.First(x=>x.IDOfProduct==0).Massa.ToString());
                            change.ShowDialog();
                            if (Helper.issuccessful)
                            {
                                UnNumarableList.First(x => x.IDOfProduct == 0).Massa = Helper.mass;
                                Helper.issuccessful = false;
                                Helper.mass = 0;
                                UpdateChecks();
                            }
                        }
                        else
                        {
                            AddNotAddedProduct addNot = new AddNotAddedProduct();
                            addNot.ShowDialog();
                            if (Helper.issuccessful == true)
                            {
                                //MessageBox.Show("fefe");
                                double sum = Helper.mass.Value;
                                ProductInCheckDto inCheckDto = new ProductInCheckDto();
                                inCheckDto.IDOfProduct = 0;
                                inCheckDto.IsNumurable = false;
                                inCheckDto.SpecialCode = "0";
                                inCheckDto.Description = "fewfe";
                                inCheckDto.Price = 1;
                                inCheckDto.Massa = sum;
                                inCheckDto.Name = "fe";
                                inCheckDto.Check = CurrentCheck;
                                UnNumarableList.Add(inCheckDto);
                                UpdateChecks();
                                Helper.issuccessful = false;
                                Helper.mass = 0;
                                Helper.count = 0;

                                Searching.Focus();
                            }
                        }
                        Searching.Clear();
                    }
                }
                else if (Comboo.Text == "За іменем")
                {
                    //GetBySpecialCode
                    string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetByName";
                    if (CheckInternetConnection.IsConnectedToInternet() == true)
                    {
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
                                            if (productDtos.Count != 0)
                                            {
                                                ProductsGrid.ItemsSource = productDtos;

                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("За даним ім'ям нічого не найдено");
                                            Searching.Focus();
                                        }
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
                        MessageBox.Show("Відсутнє підключення до інтернету!");
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
                Searching.Focus();
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
                Searching.Focus();
                //Доповнення
            }
            if (Numarable.Items.Count > 0)
            {
                var border = VisualTreeHelper.GetChild(Numarable, 0) as Decorator;
                if (border != null)
                {
                    var scroll = border.Child as ScrollViewer;
                    if (scroll != null) scroll.ScrollToEnd();
                }
            }
            if (UnNumarable.Items.Count > 0)
            {
                var border = VisualTreeHelper.GetChild(UnNumarable, 0) as Decorator;
                if (border != null)
                {
                    var scroll = border.Child as ScrollViewer;
                    if (scroll != null) scroll.ScrollToEnd();
                }
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
                    if (ToChange.IDOfProduct != 0)
                    {
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
                    else
                    {
                        ChangeNotAddedProduct change = new ChangeNotAddedProduct(ToChange.Massa.ToString());
                        change.ShowDialog();
                        if(Helper.issuccessful)
                        {
                            UnNumarableList.First(x => x.IDOfProduct == 0).Massa = Helper.mass;
                            Helper.issuccessful = false;
                            Helper.mass = 0;
                            UpdateChecks();
                        }
                       //Change added sum
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
                                        NumarableList.First(x => x.IDOfProduct == ToChange.IDOfProduct).Count = Helper.count;
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
                preCheckprod = productss;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Searching.Focus();
        }


        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Searching.Focus();
            }
            else if (e.Key == Key.F2)
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
                    LastCheck last = new LastCheck(productss, CurrentCheck);
                    last.ShowDialog();
                    if (HelperSeller.IsSuccessfull)
                    {
                        LoadCheck();
                        NumarableList.Clear();
                        UnNumarableList.Clear();
                        UpdateChecks();
                    }
                    preCheckprod = productss;


                }
            }
            else if (e.Key == Key.F3)
            {
               
            }
            else if (e.Key == Key.F4)
            {
                
            }
            else if (e.Key == Key.F5)
            {
                Button_Click_6(sender, e);
            }
        }

        void PrintPrivius()
        {
            var rpt = new Temp();
            rpt.Session = new Dictionary<string, object>();
            List<OrderItem> items = new List<OrderItem>();
            bool isnull = false;
            ProductInCheckDto productwithnull = new ProductInCheckDto();
            foreach (var item in preCheckprod)
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
                        order.Sum = string.Format("{0:0.##}", (item.Price.Value * item.Count.Value).ToString());
                        order.Count = item.Count.Value.ToString();
                    }
                    else
                    {
                        order.Sum = string.Format("{0:0.##}", (item.Price.Value * item.Massa.Value).ToString());
                        order.Count = item.Massa.Value.ToString();
                    }
                    items.Add(order);
                }
            }
            if (isnull)
            {
                items.Add(new OrderItem() { Name = "Інші продукти", Count = " ", Price = " ", Sum = string.Format("{0:0.##}", productwithnull.Massa.Value) });
            }
            else
            {

            }
            double SumM = 0;
            foreach (var item in preCheckprod)
            {
                if (item.IsNumurable == true)
                {
                    SumM += (item.Price * item.Count).Value;

                }
                else
                {
                    SumM += (item.Price * item.Massa).Value;

                }
            }
   
            rpt.Session["Model"] = new ReportModel
            {
                Sum = (Math.Round(SumM, 1)).ToString() + " грн",
                Date = DateTime.Now,
                OrderItems = items


            };

            rpt.Initialize();
            (webBrowser22.Child as System.Windows.Forms.WebBrowser).DocumentText = rpt.TransformText();
            (webBrowser22.Child as System.Windows.Forms.WebBrowser).DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted); ;

        }
        private void webBrowser1_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                (sender as System.Windows.Forms.WebBrowser).Print();
                webBrowser22.Child = new System.Windows.Forms.WebBrowser();
            }
            catch
            {
                MessageBox.Show("Принтер не підключено");
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (preCheckprod != null)
            {
               if(preCheckprod.Count!=0)
               {
                    PrintPrivius();
               }
               else
               {
                    MessageBox.Show("Попередній чек пустий");
               }
            }
            else
            {
                MessageBox.Show("Попередній чек пустий");
            }
            
        }
    }
}
