using Magazine.ModelsDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Page
    {
        public List<CalcIn> Prihods;
        public List<CalcOut> Rozhods;
        public List<Spisannya> Spisannya;
        public List<SpisannyaOnAnotherMarket> SpisannyaOnAnotherMarket;
        public List<Lesia> LesiaS;
        public List<Lena> LenaS;
        public Calculator()
        {
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            string WEBSERVICE_URL1 = StaticHelper.URL + @"api/Apii/GetAllInAndOuts";

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
                            InAndOutsDto inAndOutsDto = JsonConvert.DeserializeObject<InAndOutsDto>(jsonResponse1);
                            Prihods = inAndOutsDto.Prihods;
                            Rozhods = inAndOutsDto.Rozhods;
                            LenaS = inAndOutsDto.LenaS;
                            LesiaS = inAndOutsDto.LesiaS;
                            Spisannya = inAndOutsDto.Spisannya;
                            SpisannyaOnAnotherMarket = inAndOutsDto.SpisannyaOnAnotherMarket;
                            PrihodsGrid.ItemsSource = null;
                            PrihodsGrid.ItemsSource = Prihods;
                            RozhodsGrid.ItemsSource = null;
                            RozhodsGrid.ItemsSource = Rozhods;
                            LenaSGrid.ItemsSource = null;
                            LenaSGrid.ItemsSource = LenaS;
                            LesiaSGrid.ItemsSource = null;
                            LesiaSGrid.ItemsSource = LesiaS;
                            SpisannyaGrid.ItemsSource = null;
                            SpisannyaGrid.ItemsSource = Spisannya;
                            SpisannyaOnAnotherMarketGrid.ItemsSource = null;
                            SpisannyaOnAnotherMarketGrid.ItemsSource = SpisannyaOnAnotherMarket;
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
            Update();
        }
        private void Calc()
        {
            double PrihodsSum = 0;
            foreach (var item in Prihods)
            {
                PrihodsSum += item.Sum;
            }
                PruhidLabel.Content = PrihodsSum.ToString();
                double RozhidsSum = 0;
                foreach (var item in Rozhods)
                {
                RozhidsSum += item.Sum;
                }
            foreach (var item in Spisannya)
            {
                RozhidsSum += item.Sum;
            }
            foreach (var item in SpisannyaOnAnotherMarket)
            {
                RozhidsSum += item.Sum;
            }
            foreach (var item in LenaS)
            {
                RozhidsSum += item.Sum;
            }
            foreach (var item in LesiaS)
            {
                RozhidsSum += item.Sum;
            }
            RiznizaLabel.Content = (PrihodsSum - RozhidsSum).ToString();

            RozhidLabel.Content = RozhidsSum.ToString();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Calc();
            string WEBSERVICE_URL1 = StaticHelper.URL + $@"api/Apii/UpdateInAndOuts";

            var webRequest1 = System.Net.WebRequest.Create(WEBSERVICE_URL1);
            if (webRequest1 != null)
            {
                webRequest1.Method = "POST";
                webRequest1.Timeout = 12000;
                webRequest1.ContentType = "application/json";
                webRequest1.Headers.Add("Safety", "Safety");
                using (var streamWriter2 = new StreamWriter(webRequest1.GetRequestStream()))
                {

                    InAndOutsDto outsDto = new InAndOutsDto() { LenaS = LenaS, LesiaS = LesiaS, Rozhods = Rozhods, Prihods = Prihods, Spisannya = Spisannya, SpisannyaOnAnotherMarket = SpisannyaOnAnotherMarket };
                    var json = JsonConvert.SerializeObject(outsDto);
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
                            MessageBox.Show("Зберегли!");
                            Update();



                        }
                        catch
                        {
                            MessageBox.Show("Error");
                        }
                    }
                }
            }
        }
    }
}
