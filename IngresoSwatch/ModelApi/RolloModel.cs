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

namespace IngresoSwatch.ModelApi
{
    public class RolloModel
    {
       
        public int idrollo { get; set; }
        public string rolloName { get; set; }     
        public Nullable<int> sec { get; set; }
        public string estado { get; set; }
        public Nullable<int> idpoCont { get; set; }
        public Nullable<int> idtpc { get; set; }     
        public Nullable<double> ancho { get; set; }
        public string tono { get; set; }
        public string proceso { get; set; }
      
    }
}