using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.Services
{
    public static class CheckInternetConnection
    {
        public static bool IsConnectedToInternet()
        {
            string WEBSERVICE_URL = StaticHelper.URL;
            System.Net.WebRequest req = System.Net.WebRequest.Create(WEBSERVICE_URL);
            System.Net.WebResponse resp = default(System.Net.WebResponse);
            try
            {
                resp = req.GetResponse();
                resp.Close();
                req = null;
                return true;
            }
            catch (Exception ex)
            {
                req = null;
                return false;
            }
        }
    }
}
