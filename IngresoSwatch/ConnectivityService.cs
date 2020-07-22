using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;

namespace IngresoSwatch
{
    public class ConnectivityService
    {

        public static ConnectivityService Instance = new ConnectivityService();

        public ConnectivityService()
        {

        }

        public bool IsConnected
        {
            get
            {
                ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
                return (activeConnection != null) && activeConnection.IsConnected;
            }
        }

        public async Task<bool> IsRemoteReachable(string url)
        {
            try
            {
                URL myUrl = new URL(url);
                URLConnection connection = myUrl.OpenConnection();
                connection.ConnectTimeout = 3000;
                await connection.ConnectAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}