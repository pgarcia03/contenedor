using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IngresoSwatch.Servicios
{
    public class UrlMain
    {
        public string UrlM { get; set; }

        public UrlMain()
        {
            this.UrlM = "http://192.168.14.97:44570/api/";
        }
    }
}