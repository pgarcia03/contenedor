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
using IngresoSwatch.Adapter;
using IngresoSwatch.ModelApi;
using IngresoSwatch.Servicios;

namespace IngresoSwatch
{
    [Activity(Label = "Swatch")]
    public class Swatch : Activity
    {
        public static readonly string idcontenedor = "idcontenedor";
        public static readonly string contenedor = "contenedor";

        Spinner spinner1;
        List<RolloModel> Listarollos = new List<RolloModel>();


        EditText txtsecuenciaRollo;
        EditText txtx1,txtx2,txtx3;
        EditText txty1,txty2,txty3;
        EditText txtz1, txtz2, txtz3;
        EditText txtp1, txtp2;
      //  Button btbguardarSwatch;
        TextView lblnombrerollo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.ingresoSwatch);

            String _idconte = Intent.GetStringExtra(idcontenedor);
            String _contenedor = Intent.GetStringExtra(contenedor);

            TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
            lblnombrerollo = FindViewById<TextView>(Resource.Id.lblnombrerollo);
            spinner1 = FindViewById<Spinner>(Resource.Id.spinner1);
            txtsecuenciaRollo = FindViewById<EditText>(Resource.Id.txtsecuenciaRollo);
            txtx1= FindViewById<EditText>(Resource.Id.txtx1);
            txtx2 = FindViewById<EditText>(Resource.Id.txtx2);
            txtx3 = FindViewById<EditText>(Resource.Id.txtx3);
            txty1 = FindViewById<EditText>(Resource.Id.txty1);
            txty2 = FindViewById<EditText>(Resource.Id.txty2);
            txty3 = FindViewById<EditText>(Resource.Id.txty3);
            txtz1 = FindViewById<EditText>(Resource.Id.txtx1);
            txtz2 = FindViewById<EditText>(Resource.Id.txtx2);
            txtz3 = FindViewById<EditText>(Resource.Id.txtz3);
            txtp1 = FindViewById<EditText>(Resource.Id.txtp1);
            txtp2 = FindViewById<EditText>(Resource.Id.txtp2);

            textView1.Text = _contenedor;

            var listacod = CodigoTelaServ.GetCodigosTelaXidContenedor(int.Parse(_idconte)).Result;

            Listarollos = RolloServ.GetRollosXidContenedor(int.Parse(_idconte)).Result;

            listacod.Add(new CodigoTelaModel {
                Idtpc=0,
                Procod="Seleccione..."
            });

           // var arrayadapter = new ArrayAdapter<List<>>(this, Android.Resource.Layout.SimpleSpinnerItem, datalist);
            //simple spinner item
           // arrayadapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner1.Adapter = new CodigosTelaAdapter(this,listacod.OrderBy(x=>x.Idtpc).ToList());

            txtsecuenciaRollo.AfterTextChanged += TxtsecuenciaRollo_AfterTextChanged;

        }



        private void TxtsecuenciaRollo_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                var sec =int.Parse(txtsecuenciaRollo.Text);
              //  var spinner = spinner1.SelectedItem.ToString();
                String spinnerText = ((TextView)spinner1.FindViewById(Resource.Id.txtidcontenedor)).Text.ToString();
                var rollo = Listarollos.Where(x=>x.Sec==sec).ToList();

                if (rollo.Any())
                {
                    lblnombrerollo.Text = rollo[0].RolloName;
                    Toast.MakeText(this, "desbloquear cajas", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this,"rollo no existe",ToastLength.Short).Show();
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}