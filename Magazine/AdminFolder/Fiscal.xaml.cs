using Magazine.ModelsDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for Fiscal.xaml
    /// </summary>
    public partial class Fiscal : Page
    {
        string WEBSERVICE_URL = StaticHelper.URL;
        public Fiscal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string site = WEBSERVICE_URL+ "api/Apii/SetFiscalConnected";

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            try
            {
                using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
                {
                    string respp = stream.ReadToEnd();
                    MessageBox.Show("Готово! Оновіть.");
                }
            }
            catch
            {

            }
        }
        private void Update()
        {
            string site = WEBSERVICE_URL + "api/Apii/GetFiscalDto";

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            try
            {
                using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
                {
                    string respp = stream.ReadToEnd();
                    FiscalDto dto = JsonConvert.DeserializeObject<FiscalDto>(respp);
                    Url.Text = dto.Url;
                    IsConnected.Content = dto.IsConnected.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string site = WEBSERVICE_URL + "api/Apii/SetFiscalDisconnected";

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            try
            {
                using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
                {
                    string respp = stream.ReadToEnd();
                    MessageBox.Show("Готово! Оновіть.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FiscalDto fiscalDto = new FiscalDto();
            fiscalDto.Url = Url.Text;
            string site = WEBSERVICE_URL + "api/Apii/PostFiscalUrl";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
            req.Method = "POST";
            req.ContentType = "application/json";
            using (var streamWriter2 = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter2.Write(JsonConvert.SerializeObject(fiscalDto));
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            try
            {
                using (StreamReader stream = new StreamReader(
                resp.GetResponseStream(), Encoding.UTF8))
                {
                    string respp = stream.ReadToEnd();
                    //System.Windows.Forms.MessageBox.Show(respp);
                    MessageBox.Show("Готово! Оновіть.");
                }

            }
            catch
            {

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Ви впевнені", "Підтвердіть", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string site = WEBSERVICE_URL + "api/Apii/PrintDayReportWitoutNull";

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                try
                {
                    using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string respp = stream.ReadToEnd();
                        MessageBox.Show(respp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                //do something else
            }
            
            
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Ви впевнені", "Підтвердіть", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string site = WEBSERVICE_URL + "api/Apii/PrintDayReportWithNull";

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                try
                {
                    using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string respp = stream.ReadToEnd();
                        MessageBox.Show(respp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                //do something else
            }
            

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update(); 
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string site = WEBSERVICE_URL + "api/Apii/GetSumInFiscal";

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            try
            {
                using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
                {
                    string respp = stream.ReadToEnd();
                    MessageBox.Show(respp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Ви впевнені", "Підтвердіть", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string site = WEBSERVICE_URL + "api/Apii/GetMoneyFromSafe";

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                try
                {
                    using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string respp = stream.ReadToEnd();
                        MessageBox.Show(respp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                //do something else
            }
            
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Ви впевнені", "Підтвердіть", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string site = WEBSERVICE_URL + "api/Apii/PrintReportByProducts";

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                try
                {
                    using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string respp = stream.ReadToEnd();
                        MessageBox.Show(respp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                //do something else
            }
            
           
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Ви впевнені", "Підтвердіть", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string site = WEBSERVICE_URL + "api/Apii/PrintReportByProductsObnul";

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                try
                {
                    using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string respp = stream.ReadToEnd();
                        MessageBox.Show(respp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                //do something else
            }

            
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Ви впевнені", "Підтвердіть", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string site = WEBSERVICE_URL + "api/Apii/NullAbleCheck";

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                try
                {
                    using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string respp = stream.ReadToEnd();
                        MessageBox.Show(respp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                //do something else
            }
            
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Ви впевнені", "Підтвердіть", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    double needed = double.Parse(NeededSum.Text);
                    if (needed <= 0)
                    {
                        MessageBox.Show("Значення повинно бути більше нуля");
                    }
                    else
                    {
                        string WEBSERVICE_URL2 = StaticHelper.URL + @"api/Apii/PostSum";

                        var webRequest2 = System.Net.WebRequest.Create(WEBSERVICE_URL2);
                        if (webRequest2 != null)
                        {
                            webRequest2.Method = "POST";
                            webRequest2.Timeout = 12000;
                            webRequest2.ContentType = "application/json";
                            webRequest2.Headers.Add("Safety", "Safety");
                            using (var streamWriter2 = new StreamWriter(webRequest2.GetRequestStream()))
                            {
                                NeededSumDto neededSum = new NeededSumDto() { Sum = needed };
                                streamWriter2.Write(JsonConvert.SerializeObject(neededSum));
                            }
                            using (System.IO.Stream s2 = webRequest2.GetResponse().GetResponseStream())
                            {
                                using (var sr = new StreamReader(s2, Encoding.UTF8))
                                {
                                    MessageBox.Show("Набито на " + sr.ReadToEnd());
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Невідоме значення");
                }
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                //do something else
            }
        }
    }
}
