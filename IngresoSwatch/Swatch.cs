using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using IngresoSwatch.Adapter;
using IngresoSwatch.ModelApi;
using IngresoSwatch.Servicios;
using alert = Android.Support.V7.App.AlertDialog;

namespace IngresoSwatch
{
    [Activity(Label = "Ingreso de Medidas Swatch", Theme = "@style/AppTheme")]
    public class Swatch : AppCompatActivity
    {
        protected override void OnStart()
        {
           
            Log.Debug("OnStart", "OnStart called, app is ready to interact with the user");
            base.OnResume();
        }
        protected override void OnResume()
        {
            Log.Debug("OnResume", "OnResume called, app is ready to interact with the user");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Log.Debug("OnPause", "OnPause called, App is moving to background");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Log.Debug("OnStop", "OnStop called, App is in the background");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Listarollos.Clear();
            listacod.Clear();
            idrollo = 0;
            base.OnDestroy();
            Log.Debug("OnDestroy", "OnDestroy called, App is Terminating");
        }

        public static readonly string idcontenedor = "idcontenedor";
        public static readonly string contenedor = "contenedor";

        Spinner spinner1;
        List<RolloModel> Listarollos = new List<RolloModel>();
        List<CodigoTelaModel> listacod = new List<CodigoTelaModel>();


        EditText txtsecuenciaRollo, txtx1, txtx2, txtx3,txty1, txty2, txty3,txtp1, txtp2;
        Button btbguardarSwatch;
        TextView lblnombrerollo,lblcodigotela, lbltotalrollos;
        int idrollo=0;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.ingresoSwatch);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            String _idconte = Intent.GetStringExtra(idcontenedor);
            String _contenedor = Intent.GetStringExtra(contenedor);

            TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
            lblnombrerollo = FindViewById<TextView>(Resource.Id.lblnombrerollo);
            lblcodigotela=FindViewById<TextView>(Resource.Id.lblcodigotela);
            spinner1 = FindViewById<Spinner>(Resource.Id.spinner1);
            txtsecuenciaRollo = FindViewById<EditText>(Resource.Id.txtsecuenciaRollo);
            txtx1 = FindViewById<EditText>(Resource.Id.txtx1);
            txtx2 = FindViewById<EditText>(Resource.Id.txtx2);
            txtx3 = FindViewById<EditText>(Resource.Id.txtx3);
            txty1 = FindViewById<EditText>(Resource.Id.txty1);
            txty2 = FindViewById<EditText>(Resource.Id.txty2);
            txty3 = FindViewById<EditText>(Resource.Id.txty3);
            txtp1 = FindViewById<EditText>(Resource.Id.txtp1);
            txtp2 = FindViewById<EditText>(Resource.Id.txtp2);
            btbguardarSwatch = FindViewById<Button>(Resource.Id.btnguardarSwatch);
            lbltotalrollos = FindViewById<TextView>(Resource.Id.txttotalrollosS);

            textView1.Text = _contenedor;

            Enabled(false);
            Limpiartext();
            txtsecuenciaRollo.Enabled = false;

            listacod = await Task.Run(() => CargaListas(int.Parse(_idconte)));

            var countItem = Listarollos.Count;
            if (countItem < 1)
            {
                alert.Builder adb = new alert.Builder(this);

                adb.SetTitle("Advertencia!");
                adb.SetMessage("Ya fueron medidos de ancho todos los rollos del contenedor");
                adb.SetNeutralButton("Aceptar", (senderAlert, args) =>
                {

                });
                adb.SetPositiveButton("Buscar otro contenedor", (s, args) => {
                    StartActivity(typeof(MainActivity));
                    // SetContentView(Resource.Layout.listaRollos);
                });

                alert dialog = adb.Create();

                dialog.Show();

            }
            else
            {
               // var countItem = Listarollos.Count;
                lbltotalrollos.Text = string.Concat("Total de rollos por medir: ", countItem.ToString());
            }

            txtx1.FocusChange += Txt_FocusChange;
            txtx2.FocusChange += Txt_FocusChange;
            txtx3.FocusChange += Txt_FocusChange;
            txty1.FocusChange += Txt_FocusChange;
            txty2.FocusChange += Txt_FocusChange;
            txty3.FocusChange += Txt_FocusChange;
          
            txtp1.FocusChange += Txtp1_FocusChange;
            txtp2.FocusChange += Txtp1_FocusChange;
            //txtp1.FocusChange+=Tx
            // var arrayadapter = new ArrayAdapter<List<>>(this, Android.Resource.Layout.SimpleSpinnerItem, datalist);
            //simple spinner item
            // arrayadapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner1.Adapter = new CodigosTelaAdapter(this, listacod.OrderBy(x => x.Idtpc).ToList());
            spinner1.ItemSelected += Spinner1_ItemSelected;
            txtsecuenciaRollo.AfterTextChanged += TxtsecuenciaRollo_AfterTextChanged;
            btbguardarSwatch.Click += BtbguardarSwatch_Click;

            
        }

        private void Txtp1_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            var txt = (EditText)sender;

            if (txt.Text != string.Empty)
            {

                var resp = ValidarDiagonaltxt(txt);

                if (resp == false)
                {
                    alert.Builder adb = new alert.Builder(this);

                    adb.SetTitle("Advertencia?");
                    adb.SetMessage("Verifique el datos ingresado " + txt.Text + " y corregir!!!");
                    adb.SetNeutralButton("Mantener Dato", (senderAlert, args) =>
                    {

                    });
                    adb.SetPositiveButton("Borrar Dato Ingresado", (senderAlert, args) =>
                    {
                        txt.Text = "";
                        txt.RequestFocus();
                    });

                    alert dialog = adb.Create();

                    dialog.Show();
                }
                else
                {
                    txt.Focusable = true;
                }

            }
          
        }

        bool Validartxt(EditText txt)
        {         
            return Regex.IsMatch(txt.Text.Trim(), @"^([2]{1}[0-8]{1}.[0-9]{1,3}$|[2]{1}[0-8]{1}$)");
        }

        bool ValidarDiagonaltxt(EditText txt)
        {
            return Regex.IsMatch(txt.Text.Trim(), @"^(([2]{1}[9]{1}.[0-9]{1,3}$|[2]{1}[9]{1}$)|([3]{1}[0-5]{1}.[0-9]{1,3}$|[3]{1}[0-5]{1}$))");
        }

        void Alerta(string title, string message)
        {
            alert.Builder adb = new alert.Builder(this);

            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetNeutralButton("Aceptar", (senderAlert, args) =>
            {

            });

            alert dialog = adb.Create();

            dialog.Show();

        }
        private void BtbguardarSwatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validartxt(txtx1) && Validartxt(txtx2) && Validartxt(txtx3) &&
                    Validartxt(txty1) && Validartxt(txty2) && Validartxt(txty2) &&
                    ValidarDiagonaltxt(txtp1) && ValidarDiagonaltxt(txtp2) && idrollo!=0)
                {
                    var culture = new CultureInfo("en-US");

                    var obj = new SwatchModel()
                    {
                        Idrollo=idrollo,
                        X1 = Double.Parse(txtx1.Text, culture.NumberFormat),
                        X2 = Double.Parse(txtx2.Text, culture.NumberFormat),
                        X3 = Double.Parse(txtx3.Text, culture.NumberFormat),
                        Y1 = Double.Parse(txty1.Text, culture.NumberFormat),
                        Y2 = Double.Parse(txty2.Text, culture.NumberFormat),
                        Y3 = Double.Parse(txty3.Text, culture.NumberFormat),
                        P1 = Double.Parse(txtp1.Text, culture.NumberFormat),
                        P2 = Double.Parse(txtp2.Text, culture.NumberFormat),
                        Usuario = "MOD5",
                        Fecha = DateTimeOffset.Now
                    };

                    var resp = SwatchServ.SaveSwatch(obj).Result;


                    if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Listarollos.RemoveAt(Listarollos.FindIndex(x=>x.Idrollo==idrollo));
                        txtsecuenciaRollo.Text = string.Empty;
                        lblnombrerollo.Text = "";
                        Toast.MakeText(this,"Ingreso Correcto",ToastLength.Long).Show();
                        idrollo = 0;
                        Limpiartext();
                        Enabled(false);

                        var countItem = Listarollos.Count;
                        lbltotalrollos.Text = string.Concat("Total de rollos por medir: ", countItem.ToString());
                    }
                }
                else
                {
                    Toast.MakeText(this, "llenar correctamente", ToastLength.Long).Show();
                }



            }
            catch (Exception ex)
            {
                Alerta("Advertencia?", ex.Message);

            }
        }

        private void Spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;


            if (spinner.SelectedItemPosition != 0)
            {
                String spinnerText = ((TextView)spinner1.FindViewById(Resource.Id.txtnombrecontenedor)).Text.ToString();
                lblcodigotela.Text = spinnerText;
                txtsecuenciaRollo.Enabled = true;
                txtsecuenciaRollo.Text = "";
                txtsecuenciaRollo.Focusable = true;
            }
        }

        void Limpiartext()
        {
            txtx1.Text = "";
            txtx2.Text = "";
            txtx3.Text = "";
            txty1.Text = "";
            txty2.Text = "";
            txty3.Text = "";
            txtp1.Text = "";
            txtp2.Text = "";
        }

      
        void Enabled(bool valor)
        {
            txtx1.Enabled = valor;
            txtx2.Enabled = valor;
            txtx3.Enabled = valor;
            txty1.Enabled = valor;
            txty2.Enabled = valor;
            txty3.Enabled = valor;
            txtp1.Enabled = valor;
            txtp2.Enabled = valor;
        }

        private void Txt_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            var txt = (EditText)sender;

            if (txt.Text != string.Empty)
            {

                var resp = Validartxt(txt);// Regex.IsMatch(txt.Text.Trim(), @"^[2-3][0-9]{0,2}([\.]{1}[0-9]{1,3}|[\.]{0})$");

                if (resp==false)
                {
                    alert.Builder adb = new alert.Builder(this);

                    adb.SetTitle("Advertencia?");
                    adb.SetMessage("Verifique el datos ingresado "+ txt.Text +" y corregir!!!");
                    adb.SetNeutralButton("Mantener Dato", (senderAlert, args) =>
                    {
                        
                    });
                    adb.SetPositiveButton("Borrar Dato Ingresado", (senderAlert, args) =>
                    {
                        txt.Text = "";
                        txt.RequestFocus();
                    });

                    alert dialog = adb.Create();

                    dialog.Show();

                }
                else
                {
                    txt.Focusable = true;
                }
                
            }
         
        }

        async Task<List<CodigoTelaModel>> CargaListas(int _idconte)
        {

            var tarea1 = Task.Run(() => { return CodigoTelaServ.GetCodigosTelaXidContenedor(_idconte); });

            var tarea2 = Task.Run(() => { return RolloServ.GetRollosXidContenedor(_idconte); });

            await Task.WhenAll(tarea1, tarea2);

            var listacodTem = tarea1.Result;
            Listarollos = tarea2.Result;

            listacodTem.Add(new CodigoTelaModel
            {
                Idtpc = 0,
                Procod = "Seleccione..."
            });

         

            return listacodTem;
        }

        private void TxtsecuenciaRollo_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (txtsecuenciaRollo.Text.Trim() == string.Empty)
                {
                    idrollo = 0;
                    lblnombrerollo.Text = string.Empty;
                    Limpiartext();
                    Enabled(false);
                   
                    return;
                }
                var sec = int.Parse(txtsecuenciaRollo.Text);

                String spinnerText = ((TextView)spinner1.FindViewById(Resource.Id.txtidcontenedor)).Text.ToString();

                if (spinnerText == "0")
                {
                    Alerta("Advertencia?", "Debe Seleccionar El Codigo De Tela ");
                   

                    return;
                }

                var rollo = Listarollos.Where(x => x.Sec == sec && x.Idtpc == int.Parse(spinnerText)).ToList();

                if (rollo.Any())
                {
                    lblnombrerollo.Text = rollo[0].RolloName;
                    idrollo =int.Parse(rollo[0].Idrollo.ToString());
                    Enabled(true);
                 
                }
                else
                {
                    idrollo = 0;
                    lblnombrerollo.Text = string.Empty;
                    Enabled(false);
                    Toast.MakeText(this, "El rollo no existe o ya se ingresaron sus medidas", ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Alerta("Advertencia?", ex.Message);

            }
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}