using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using IngresoSwatch.Servicios;
using IngresoSwatch.Adapter;
using System.Collections.Generic;
using IngresoSwatch.ModelApi;
using alert = Android.Support.V7.App.AlertDialog;
using Android.Content;
using System.Threading.Tasks;
using Android.Util;
using System.Linq;

namespace IngresoSwatch
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnStart()
        {
            editText1.Text = string.Empty;

            var conn = new ConnectivityService();

            if (conn.IsConnected)
            {
                Task.Run(async () => { await ListaContenedor(); }); //ContenedorServ.GetContenedor().Result;
            }
            else
            {
                Alerta("Advertencia?", "Verifique su conexion a la red wifi");
            }

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
            editText1.Text = string.Empty;

            base.OnDestroy();
            Log.Debug("OnDestroy", "OnDestroy called, App is Terminating");
        }

        EditText editText1;
        ListView listView1;
        //AutoCompleteTextView txtsearch;
        List<ContenedorModel> list = new List<ContenedorModel>();
        List<ContenedorModel> listtemp = new List<ContenedorModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.listaRollos);

            // list = ContenedorServ.GetContenedorXnombre().Result;

            editText1 = FindViewById<EditText>(Resource.Id.editText1);
            listView1 = FindViewById<ListView>(Resource.Id.listView1);

            editText1.TextChanged += EditText1_TextChanged;
            listView1.ItemClick += ListView1_ItemClick;

        }

        private void ListView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {

                var obj = listtemp[e.Position];

                alert.Builder adb = new alert.Builder(this);

                adb.SetTitle("Registro de Medidas?");
                adb.SetMessage("Esta Seguro de Seleccionar el contenedor ");
                //  adb.SetPositiveButton()
                adb.SetPositiveButton("Medida Swatch", (senderAlert, args) =>
                {

                    Intent i = new Intent(this, typeof(Swatch));
                    i.PutExtra(Swatch.idcontenedor, obj.Idcontenedor.ToString());
                    i.PutExtra(Swatch.contenedor, obj.Contenedor);
                    // startActivity(i);
                    StartActivity(i);

                });
                adb.SetNegativeButton("Medida Ancho", (senderAlert, args) =>
                {

                    Intent i = new Intent(this, typeof(Width));
                    i.PutExtra(Width.idcontenedor, obj.Idcontenedor.ToString());
                    i.PutExtra(Width.contenedor, obj.Contenedor);
                    // startActivity(i);
                    StartActivity(i);

                    //Toast.MakeText(this, "Elimacion cancelada", ToastLength.Short).Show();
                });
                adb.SetNeutralButton("Cancelar", (senderAlert, args) =>
                {

                    Toast.MakeText(this, "Seleccion Cancelada", ToastLength.Short).Show();
                });

                alert dialog = adb.Create();

                dialog.Show();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void EditText1_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                var num = editText1.Text.Length;
                if (num > 0)
                {
                    listtemp = list.Where(x => x.Contenedor.ToUpper().Contains(editText1.Text.ToUpper())).ToList();
                    listView1.Adapter = new AutocompleteContenedorAdapter(this, listtemp);
                }
                else
                {
                    listtemp.Clear();// = ContenedorServ.GetContenedorXnombre(editText1.Text.TrimEnd()).Result;
                    //var listtemp = new List<ContenedorModel>();
                    listView1.Adapter = new AutocompleteContenedorAdapter(this, listtemp);
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        async Task<List<ContenedorModel>> ListaContenedor(string pre)
        {
            return await Task.Run(() =>
            {
                return ContenedorServ.GetContenedorXnombre(editText1.Text.TrimEnd());
            });
        }

        async Task ListaContenedor()
        {
            await Task.Run(() =>
            {
                list = ContenedorServ.GetContenedor().Result;
            });
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


        //private void Txtsearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        //{
        //    var num = txtsearch.Text.Length;

        //    if (num > 3)
        //    {
        //        list = ContenedorServ.GetContenedorXnombre(txtsearch.Text.TrimEnd()).Result;

        //        //              var adapter = new ArrayAdapter<String>(this, Resource.Layout.autocomContenedor, list);
        //        //                listViewNumeros.Adapter = new ListnumerosAdapter(this, listanumeros);

        //        txtsearch.Adapter = new AutocompleteContenedorAdapter(this, list);

        //    }
        //}

        //private void Txtsearch_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    var num = e.Position;

        //    var id = list[e.Position].Idcontenedor;
        //}

        //        private void TextView_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        //        {
        //            var num=txtsearch.Text.Length;

        //            if (num>3)
        //            {
        //                 list = ContenedorServ.GetContenedorXnombre(txtsearch.Text.TrimEnd()).Result;

        //  //              var adapter = new ArrayAdapter<String>(this, Resource.Layout.autocomContenedor, list);
        ////                listViewNumeros.Adapter = new ListnumerosAdapter(this, listanumeros);

        //                txtsearch.Adapter = new AutocompleteContenedorAdapter(this, list);

        //            }

        //        }
    }
}