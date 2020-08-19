using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using IngresoSwatch.ModelApi;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using alert = Android.Support.V7.App.AlertDialog;
using IngresoSwatch.Servicios;
using System.Threading.Tasks;
using IngresoSwatch.Adapter;
using System.Globalization;

namespace IngresoSwatch
{
    [Activity(Label = "Ingreso de Medidas de Ancho ", Theme = "@style/AppTheme")]
    public class Width : AppCompatActivity
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
            ListarollosWidth.Clear();
            listacodWidth.Clear();
            idrolloWidth = 0;
            base.OnDestroy();
            Log.Debug("OnDestroy", "OnDestroy called, App is Terminating");
        }

        public static readonly string idcontenedor = "idcontenedor";
        public static readonly string contenedor = "contenedor";

        Spinner spinner1Width;
        List<RolloModel> ListarollosWidth = new List<RolloModel>();
        List<CodigoTelaModel> listacodWidth = new List<CodigoTelaModel>();


        EditText txtsecuenciaRolloWidth, txtWidth;
        Button btbguardarWidth;
        TextView lblnombrerolloWidth, lblcodigotelaWidth,lbltotalrollos;
        int idrolloWidth = 0;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ingresoWidth);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            String _idconte = Intent.GetStringExtra(idcontenedor);
            String _contenedor = Intent.GetStringExtra(contenedor);

            TextView textViewWidth = FindViewById<TextView>(Resource.Id.textViewWidth);
            lblnombrerolloWidth = FindViewById<TextView>(Resource.Id.lblnombrerolloWidth);
            lblcodigotelaWidth = FindViewById<TextView>(Resource.Id.lblcodigotelaWidth);
            spinner1Width = FindViewById<Spinner>(Resource.Id.spinner1Width);
            txtsecuenciaRolloWidth = FindViewById<EditText>(Resource.Id.txtsecuenciaRolloWidth);
            txtWidth = FindViewById<EditText>(Resource.Id.txtWidth);
            lbltotalrollos = FindViewById<TextView>(Resource.Id.txttotalrollosW);

            btbguardarWidth = FindViewById<Button>(Resource.Id.btnguardarWidth);

            textViewWidth.Text = _contenedor;

            Enabled(false);
            Limpiartext();
            txtsecuenciaRolloWidth.Enabled = false;

            listacodWidth = await Task.Run(() => CargaListas(int.Parse(_idconte)));

            var countItem = ListarollosWidth.Count;
            if (countItem<1)
            {
                alert.Builder adb = new alert.Builder(this);

                adb.SetTitle("Advertencia!");
                adb.SetMessage("Ya fueron medidos de ancho todos los rollos del contenedor");
                adb.SetNeutralButton("Aceptar", (senderAlert, args) =>
                {

                });
                adb.SetPositiveButton("Buscar otro contenedor",(s,args)=> {
                    StartActivity(typeof(MainActivity));
                   // SetContentView(Resource.Layout.listaRollos);
                });

                alert dialog = adb.Create();

                dialog.Show();
                
            }
            else
            {
                lbltotalrollos.Text = string.Concat("Total de rollos por medir: ", countItem.ToString());
            }

            txtWidth.FocusChange += TxtWidth_FocusChange;

            spinner1Width.Adapter = new CodigosTelaAdapter(this, listacodWidth.OrderBy(x => x.Idtpc).ToList());
            spinner1Width.ItemSelected += Spinner1Width_ItemSelected; ;
            txtsecuenciaRolloWidth.AfterTextChanged += TxtsecuenciaRolloWidth_AfterTextChanged;
            btbguardarWidth.Click += BtbguardarWidth_Click;

        }

        private void TxtWidth_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            var txt = (EditText)sender;

            if (txt.Text != string.Empty)
            {

                var resp = Validartxt(txt);

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
            return Regex.IsMatch(txt.Text.Trim(), @"^([3-8]{1}[0-9]{1}.[0-9]{1,3}$|[3-8]{1}[0-9]{1}$)");
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

        private async void BtbguardarWidth_Click(object sender, EventArgs e)
        {

            var progress = new ProgressDialog(this)
            {
                Indeterminate = true
            };
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading... Please wait...");
            progress.SetCancelable(false);

            try
            {

                if (Validartxt(txtWidth) && idrolloWidth != 0)
                {

                    var culture = new CultureInfo("en-US");
                  
                    var obj = new RolloModel()
                    {
                        Idrollo = idrolloWidth,
                        Ancho=double.Parse(txtWidth.Text,culture.NumberFormat)
                    };

                    var conection = new ConnectivityService();

                    if (conection.IsConnected)
                    {
                        progress.Show();

                        var resp = await Task.Run(()=> { return RolloServ.UpdateRollo(obj).Result; });


                        if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ListarollosWidth.RemoveAt(ListarollosWidth.FindIndex(x => x.Idrollo == idrolloWidth));
                            txtsecuenciaRolloWidth.Text = string.Empty;
                            lblnombrerolloWidth.Text = "";
                            Toast.MakeText(this, "Ingreso Correcto", ToastLength.Long).Show();
                            idrolloWidth = 0;
                            Limpiartext();
                            Enabled(false);

                            var countItem = ListarollosWidth.Count;
                            lbltotalrollos.Text = string.Concat("Total de rollos por medir: ", countItem.ToString());
                            txtsecuenciaRolloWidth.RequestFocus();

                            progress.Dismiss();
                        }
                        else
                        {
                            progress.Dismiss();
                            Alerta("Advertencia?", "Ha ocurrido un error, no se efectuaron los cambios!!!");
                        }

                    }
                    else
                    {
                        progress.Dismiss();
                        Alerta("Advertencia?", "Verifique su conexion a la red wifi");
                    }

                }
                else
                {
                    progress.Dismiss();
                    Toast.MakeText(this, "llenar correctamente", ToastLength.Long).Show();
                }



            }
            catch (Exception ex)
            {

                progress.Dismiss();
                Alerta("Advertencia?", ex.Message);

            }
        }

        void Limpiartext()
        {
            txtWidth.Text = "";
           
        }
       
        void Enabled(bool valor)
        {
            txtWidth.Enabled = valor;           
        }

        async Task<List<CodigoTelaModel>> CargaListas(int _idconte)
        {

            var tarea1 = Task.Run(() => { return CodigoTelaServ.GetCodigosTelaXidContenedor(_idconte); });

            var tarea2 = Task.Run(() => { return RolloServ.GetRollosXidContenedor_ancho(_idconte); });

            await Task.WhenAll(tarea1, tarea2);

            var listacodTem = tarea1.Result;
            ListarollosWidth = tarea2.Result;

            listacodTem.Add(new CodigoTelaModel
            {
                Idtpc = 0,
                Procod = "Seleccione..."
            });

            return listacodTem;
        }


        private void TxtsecuenciaRolloWidth_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (txtsecuenciaRolloWidth.Text.Trim() == string.Empty)
                {
                    idrolloWidth = 0;
                    lblnombrerolloWidth.Text = string.Empty;
                    Limpiartext();
                    Enabled(false);

                    return;
                }
                var sec = int.Parse(txtsecuenciaRolloWidth.Text);

                String spinnerText = ((TextView)spinner1Width.FindViewById(Resource.Id.txtidcontenedor)).Text.ToString();

                if (spinnerText == "0")
                {
                    Alerta("Advertencia?", "Debe Seleccionar El Codigo De Tela ");
                   
                    return;
                }

                var rollo = ListarollosWidth.Where(x => x.Sec == sec && x.Idtpc == int.Parse(spinnerText)).ToList();

                if (rollo.Any())
                {
                    lblnombrerolloWidth.Text = rollo[0].RolloName;
                    idrolloWidth = int.Parse(rollo[0].Idrollo.ToString());
                    Enabled(true);

                }
                else
                {
                    idrolloWidth = 0;
                    lblnombrerolloWidth.Text = string.Empty;
                    Enabled(false);

                    Toast.MakeText(this, "El rollo no existe o ya se ingresaron sus medidas", ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Alerta("Advertencia?", ex.Message);

            }
        }

        private void Spinner1Width_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;


            if (spinner.SelectedItemPosition != 0)
            {
                String spinnerText = ((TextView)spinner1Width.FindViewById(Resource.Id.txtnombrecontenedor)).Text.ToString();
                lblcodigotelaWidth.Text = spinnerText;
                txtsecuenciaRolloWidth.Enabled = true;
                txtsecuenciaRolloWidth.Text = "";
                txtsecuenciaRolloWidth.Focusable = true;
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