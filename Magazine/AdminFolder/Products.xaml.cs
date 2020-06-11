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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magazine.AdminFolder
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        List<ProductDto> products = new List<ProductDto>();
        bool ischange = false;
        bool IsActive = false;
        bool IsNumarableCurrent = false;
        ProductDto curent;
        public Products()
        {
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetProducts";

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
                            List<ProductDto> videogames = JsonConvert.DeserializeObject<List<ProductDto>>(jsonResponse1);
                            products.Clear();
                           // MessageBox.Show(products.Count.ToString());
                            products.AddRange(videogames);
                        }
                        catch
                        {
                            MessageBox.Show("Error");
                        }
                    }
                }
            }
     
            ProductsGrid.ItemsSource = null;
            ProductsGrid.ItemsSource = products;
        }
        private bool IsExists()
        {
            bool isexits = true;
            string WEBSERVICE_URL1 = StaticHelper.URL + $@"api/Apii/IsExists";

            var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
            if (webRequest1 != null)
            {
                webRequest1.Method = "POST";
                webRequest1.Timeout = 12000;
                webRequest1.ContentType = "application/json";
                webRequest1.Headers.Add("Safety", "Safety");
                using (var streamWriter2 = new StreamWriter(webRequest1.GetRequestStream()))
                {

                    var json = JsonConvert.SerializeObject(Code.Text);
                    streamWriter2.Write(json);
                }
                //webRequest1.Headers.Add("SpecialCode", Code.Text);
                using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                    {
                        try
                        {
                            var jsonResponse1 = sr1.ReadToEnd();

                            isexits = bool.Parse(jsonResponse1);



                        }
                        catch
                        {
                            MessageBox.Show("Error");
                        }
                    }
                }
            }
            return isexits;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("Changed");
            // Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
            // Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
            kek(sender);
        }
        private void kek(object sender)
        {
            UnNumarable.Visibility=Visibility.Visible;
            Numarable.Visibility = Visibility.Visible;
            string text = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            if (text == "Додати новий")
            {
                //іMessageBox.Show("");
               
                AddNew.Visibility = Visibility.Visible;
                ChangeCurrent.Visibility = Visibility.Hidden;
                ButtonsAdd.Visibility = Visibility.Visible;
                ButtonsChange.Visibility = Visibility.Hidden;
                combo2.Text = "Кількість";
                Code.Focus();
                ischange = false;
            }
            else if (text == "Змінити існуючий")
            {
                AddNew.Visibility = Visibility.Hidden;
                ChangeCurrent.Visibility = Visibility.Visible;
                ButtonsAdd.Visibility = Visibility.Hidden;
                ButtonsChange.Visibility = Visibility.Visible;
                ischange = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Code.Focus();
        }

        private void Combo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Value.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Code.Text = "";
            NameOfProduct.Text = "";
            ShortDescription.Text = "";
            combo2.Text = "";
            Value.Text = "";
            Price.Text = "";
            Type.Text = "";
            ChangePrice.Text = "";
        }
        
        void ClearAll()
        {
            Code.Text = "";
            NameOfProduct.Text = "";
            ShortDescription.Text = "";

            combo2.Text = "";
            Value.Text = "";
            Price.Text = "";
            Type.Text = "";
            ChangePrice.Text = "";
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            if(Code.Text != "")
            {
                if (NameOfProduct.Text != "")
                {
                    
                        if(combo2.Text!="")
                        {
                            if(Value.Text!=""&& Value.Text != "0")
                            {
                            //TODO Чи є в бд з таким кодом
                            /*string WEBSERVICE_URL2 = StaticHelper.URL + @"api/Apii/ChangeProduct";

            var webRequest2 = System.Net.WebRequest.Create(WEBSERVICE_URL2);
            if (webRequest2 != null)
            {
                webRequest2.Method = "POST";
                webRequest2.Timeout = 12000;
                webRequest2.ContentType = "application/json";
                webRequest2.Headers.Add("Safety", "Safety");
                using (var streamWriter2 = new StreamWriter(webRequest2.GetRequestStream()))
                {

                    var json = JsonConvert.SerializeObject(productDto);
                    streamWriter2.Write(json);
                }
                //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                using (System.IO.Stream s2 = webRequest2.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr2 = new System.IO.StreamReader(s2))
                    {
                        try
                        {
                            var jsonResponse1 = sr2.ReadToEnd();
                            //List<UserDto> videogames = JsonConvert.DeserializeObject<List<UserDto>>(jsonResponse1);
                            //users.Clear();
                            //users.AddRange(videogames);
                            MessageBox.Show("Змінено!");
                            ClearAll();
                            Update();
                        }
                        catch
                        {

                        }
                    }
                }
            }*/
                            //bool isexits = true;
                            //string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/IsExists";

                            //var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
                            //if (webRequest1 != null)
                            //{
                            //webRequest1.Method = "POST";
                            //webRequest1.Timeout = 12000;
                            //    webRequest1.ContentType = "application/json";
                            //    webRequest1.Headers.Add("Safety", "Safety");

                            //webRequest1.ContentType = "application/json";
                            //webRequest1.Headers.Add("SpecialCode", Code.Text);

                            ////Include English in the Accept-Langauge header. 
                            //myWebHeaderCollection.Add("Accept-Language", "en;q=0.8");
                            //using (var streamWriter2 = new StreamWriter(webRequest1.GetRequestStream()))
                            //{

                            //    var json = Code.Text;
                            //    streamWriter2.Write(json);
                            //}
                            //using (System.IO.Stream s1 = webRequest1.GetResponse().GetResponseStream())
                            //    {
                            //        using (System.IO.StreamReader sr1 = new System.IO.StreamReader(s1))
                            //        {
                            //            try
                            //            {
                            //                var jsonResponse1 = sr1.ReadToEnd();

                            //                isexits = bool.Parse(jsonResponse1);



                            //            }
                            //            catch
                            //            {
                            //                MessageBox.Show("Error");
                            //            }
                            //        }
                            //    }
                            //}
                            bool isexits = IsExists();
                                //TODO Чи валідне значення
                                if (isexits != true)
                                {
                                    bool isvalid = false;
                                bool isvalid2 = false;
                                int num1 = 0;
                                    double num2 = -1;
                                    double price=0;
                                    if (combo2.Text == "Кількість")
                                    {
                                        int num;
                                        bool isInt = int.TryParse(Value.Text, out num);
                                        if (isInt)
                                        {
                                        if (num > 0)
                                        {
                                            isvalid = true;
                                            num1 = num;
                                        }
                                            //MessageBox.Show("Double");
                                            // double here
                                        }
                                        else
                                        {
                                            MessageBox.Show("Число не є коректним. Приклад числа: 3");
                                            isvalid = false;
                                        }           
                                    }
                                    else
                                    {
                                        double num;
                                        bool isDouble = Double.TryParse(Value.Text, out num);
                                        if (isDouble)
                                        {
                                        if (num > 0)
                                        {
                                            isvalid = true;
                                            num2 = num;
                                        }
                                            //MessageBox.Show("Double");
                                            // double here
                                        }
                                        else
                                        {
                                            MessageBox.Show("Число не є коректним. Приклад числа: 3,14");
                                        }
                                    }
                                    double num11;
                                    bool isDouble1 = Double.TryParse(Price.Text, out num11);
                                    if (isDouble1)
                                    {
                                        if (num11 > 0)
                                        {
                                        isvalid2 = true;
                                        price = num11;
                                        //MessageBox.Show("Double");
                                        // double here
                                        }
                                    }
                                    else
                                    {
                                    isvalid2 = false;
                                        MessageBox.Show("Ціна є не коректою. Приклад коректої: 3,14");
                                    }
                                    if (isvalid2==true&&isvalid==true)
                                    {

                                        ProductDto product = new ProductDto();
                                        if (ShortDescription.Text != "")
                                        {
                                        product.Description = ShortDescription.Text;
                                        }
                                        else
                                    {
                                        product.Description = ".";
                                    }
                                        product.Name = NameOfProduct.Text;
                                        product.SpecialCode = Code.Text;
                                        product.Description = ShortDescription.Text;
                                        product.Price = price;
                                        if (num2 != -1)
                                        {
                                            product.IsNumurable = false;
                                            product.Massa = num2;
                                        }
                                        else
                                        {
                                            product.IsNumurable = true;
                                            product.Count = num1;
                                        }
                                        string WEBSERVICE_URL2 = StaticHelper.URL + @"api/Apii/AddProduct";

                                        var webRequest2 = System.Net.WebRequest.Create(WEBSERVICE_URL2);
                                        if (webRequest2 != null)
                                        {
                                            webRequest2.Method = "POST";
                                            webRequest2.Timeout = 12000;
                                            webRequest2.ContentType = "application/json";
                                            webRequest2.Headers.Add("Safety", "Safety");
                                            using (var streamWriter2 = new StreamWriter(webRequest2.GetRequestStream()))
                                            {

                                                var json = JsonConvert.SerializeObject(product);
                                                streamWriter2.Write(json);
                                            }
                                            //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                                            using (System.IO.Stream s2 = webRequest2.GetResponse().GetResponseStream())
                                            {
                                                using (System.IO.StreamReader sr2 = new System.IO.StreamReader(s2))
                                                {
                                                    try
                                                    {
                                                        var jsonResponse1 = sr2.ReadToEnd();
                                                        //List<UserDto> videogames = JsonConvert.DeserializeObject<List<UserDto>>(jsonResponse1);
                                                        //users.Clear();
                                                        //users.AddRange(videogames);
                                                        MessageBox.Show("Додано!");
                                                        ClearAll();
                                                         Update();
                                                    combo2.Text = "Кількість";
                                                    Code.Focus();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                }
                                            }
                                        }
                                        //TODO REQUEST ADD PRODUCT
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Даний код уже існує");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Величина не може дорівнювати нулю!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ви не вибрали тип!");
                        }
                    
                }
                else
                {
                    MessageBox.Show("Назва продукту не може бути пустою.");
                }
            }
            else
            {
                MessageBox.Show("Код не може бути пустим!");
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Numarable.Height = 0;
            UnNumarable.Height=0;
            if (ProductsGrid.SelectedItem != null)
            {
                ProductDto item = (ProductDto)ProductsGrid.SelectedItem;
                if (ischange == true)
                {
                    IsActive = true;
                    if (item.IsNumurable == true)
                    {
                        Numarable.Height = 50;
                        IsNumarableCurrent = true;
                        OldValue.Text = item.Count.ToString();
                        NewValue.Text = "0";
                        //MessageBox.Show("lol");
                    }
                    else
                    {
                        UnNumarable.Height = 50;
                        IsNumarableCurrent = false;
                        OldValue.Text = item.Massa.ToString();
                        NewMassa.Text = "0";
                        //MessageBox.Show("kek");
                    }
                    ChangePrice.Text = item.Price.ToString();
                    ChangeCode.Text = item.SpecialCode;
                    ChangeNameOfProduct.Text = item.Name;
                    ChangeShortDescription.Text = item.Description;
                    if (item.IsNumurable == true)
                    {
                        Type.Text = "Кількість:";
                    }
                    else
                    {
                        Type.Text = "Кілограми:";
                    }
                    curent = item;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ProductDto currentObject = (ProductDto)ProductsGrid.SelectedItem;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ви впевнені що бажаєте видалити " , "Підтвердження дії", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/DeleteProductById";

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
                                MessageBox.Show("Успішне видалення");
                                Update();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                
                //TODO DELETE
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ChangeCode.Text = "";
            ChangeCode.Focus();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ChangeCode.Text = "";
            ChangeNameOfProduct.Text = "";
            ChangeShortDescription.Text = "";
            OldValue.Text = "";
            NewValue.Text = "";
            NewMassa.Text = "";
            ChangePrice.Text = "";
            curent = null;
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = int.Parse(NewValue.Text);
                curent.Count += num;
                OldValue.Text = curent.Count.ToString();
            }
            catch
            {

            }
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = int.Parse(NewValue.Text);
                curent.Count -= num;
                OldValue.Text = curent.Count.ToString();
            }
            catch
            {

            }
        }

        private void PlusMass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = double.Parse(NewMassa.Text);
                curent.Massa += num;
                OldValue.Text = curent.Massa.ToString();
            }
            catch
            {

            }
        }

        private void MinusMass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = double.Parse(NewMassa.Text);
            curent.Massa -= num;
            OldValue.Text = curent.Massa.ToString();
            }
            catch
            {

            }
        }

        private void Upddate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (curent != null)
            {
                ProductDto productDto = new ProductDto();
                productDto.ID = curent.ID;
                productDto.IsNumurable = curent.IsNumurable;
                productDto.CameToTheStorage = curent.CameToTheStorage;
                productDto.SpecialCode = ChangeCode.Text;
                productDto.Name = ChangeNameOfProduct.Text;
                productDto.Description = ChangeShortDescription.Text;

                productDto.Price =double.Parse(ChangePrice.Text);
                if (curent.IsNumurable == true)
                {
                    productDto.Count = int.Parse(OldValue.Text);
                }
                else
                {
                    productDto.Massa = double.Parse(OldValue.Text);
                }
                string WEBSERVICE_URL2 = StaticHelper.URL + @"api/Apii/ChangeProduct";

                var webRequest2 = System.Net.WebRequest.Create(WEBSERVICE_URL2);
                if (webRequest2 != null)
                {
                    webRequest2.Method = "POST";
                    webRequest2.Timeout = 12000;
                    webRequest2.ContentType = "application/json";
                    webRequest2.Headers.Add("Safety", "Safety");
                    using (var streamWriter2 = new StreamWriter(webRequest2.GetRequestStream()))
                    {

                        var json = JsonConvert.SerializeObject(productDto);
                        streamWriter2.Write(json);
                    }
                    //webRequest.Headers.Add("StoreData", JsonConvert.SerializeObject(store));
                    using (System.IO.Stream s2 = webRequest2.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr2 = new System.IO.StreamReader(s2))
                        {
                            try
                            {
                                var jsonResponse1 = sr2.ReadToEnd();
                                //List<UserDto> videogames = JsonConvert.DeserializeObject<List<UserDto>>(jsonResponse1);
                                //users.Clear();
                                //users.AddRange(videogames);
                                MessageBox.Show("Змінено!");
                                ClearAll();
                                Update();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                ChangeCode.Text = "";
                ChangeNameOfProduct.Text = "";
                ChangeShortDescription.Text = "";
                OldValue.Text = "";
                NewValue.Text = "";
                NewMassa.Text = "";
                curent = null;
            }
            
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ProductsGrid.ItemsSource = null;
            Filter.Text = "";
            Update();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string filter = Filter.Text.ToLower();
            List<ProductDto> productDtos = new List<ProductDto>();
            for(int i=0; i< products.Count; i++)
            {
                bool isfounded = false;
                try
                {
                    if (products[i].Count.ToString().ToLower().Contains(filter))
                    {
                        productDtos.Add(products[i]);
                        isfounded = true;
                    }
                }
                catch
                {

                }
                try
                {
                    if(!isfounded)
                    {
                        if (products[i].Massa.ToString().ToLower().Contains(filter))
                        {
                            productDtos.Add(products[i]);
                            isfounded = true;
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    if (!isfounded)
                    {
                        if (products[i].Name.ToLower().Contains(filter))
                        {
                            productDtos.Add(products[i]);
                            isfounded = true;
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    if (!isfounded)
                    {
                        if (products[i].SpecialCode.ToLower().Contains(filter))
                        {
                            productDtos.Add(products[i]);
                            isfounded = true;
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    if (!isfounded)
                    {
                        if (products[i].Description.ToLower().Contains(filter))
                        {
                            productDtos.Add(products[i]);
                            isfounded = true;
                        }
                    }
                }
                catch
                {
                   
                }
                try
                {
                    if (!isfounded)
                    {
                        if (products[i].Price.ToString().ToLower().Contains(filter))
                        {
                            productDtos.Add(products[i]);
                            isfounded = true;
                        }
                    }
                }
                catch
                {

                }

            }
           
            if(productDtos.Count==0)
            {
                MessageBox.Show("По заданому фільтру нічого не знайдено!");
            }
            else
            {
                ProductsGrid.ItemsSource = null;
                ProductsGrid.ItemsSource = productDtos;
            }
        }

        private void Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
              
                if(IsExists()==true)
                {
                    MessageBox.Show("Данний код уже є в базі.");
                    
                    ChangeMenu();
                }
                else
                {
                    NameOfProduct.Focus();
                }
            }
        }

        private void NameOfProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShortDescription.Focus();
            }
        }

        private void ShortDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Value.Focus();
            }
        }

        private void Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Price.Focus();
            }
        }

        private void Price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click_2(sender, e);
            }
        }

        private void Filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click_7(sender, e);
            }
        }
        private void ChangeMenu()
        {
            AddNew.Visibility = Visibility.Hidden;
            ChangeCurrent.Visibility = Visibility.Visible;
            ButtonsAdd.Visibility = Visibility.Hidden;
            ButtonsChange.Visibility = Visibility.Visible;
            ischange = true;
            combo.Text = "Змінити існуючий";
            ProductDto item = products.First(x=>x.SpecialCode==Code.Text);
            
            //ClearAll();
            if (ischange == true)
            {
                IsActive = true;
                if (item.IsNumurable == true)
                {
                    Numarable.Height = 50;
                    IsNumarableCurrent = true;
                    OldValue.Text = item.Count.ToString();
                    NewValue.Text = "0";
                    UnNumarable.Height = 0;
                    //MessageBox.Show("lol");
                }
                else
                {
                    UnNumarable.Height = 50;
                    IsNumarableCurrent = false;
                    OldValue.Text = item.Massa.ToString();
                    NewMassa.Text = "0";
                    Numarable.Height = 0;
                    //MessageBox.Show("kek");
                }
                ChangePrice.Text = item.Price.ToString();
                ChangeCode.Text = item.SpecialCode;
                ChangeNameOfProduct.Text = item.Name;
                ChangeShortDescription.Text = item.Description;
                if (item.IsNumurable == true)
                {
                    Type.Text = "Кількість:";
                }
                else
                {
                    Type.Text = "Кілограми:";
                }
                curent = item;
            }
            Code.Text = "";
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
          
            OldValue.Text = "0";
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            OldValue.Text = String.Format("{0:0.00}", double.Parse( OldValue.Text)); 
        }
    }
    
}
