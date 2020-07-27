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

namespace IngresoSwatch
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText editText1;
        ListView listView1;
        //AutoCompleteTextView txtsearch;
        List<ContenedorModel> list=new List<ContenedorModel>();
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
                var obj = list[e.Position];

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
                adb.SetNegativeButton("Medida Ancho", (senderAlert, args) => {

                    Intent i = new Intent(this, typeof(Width));
                    i.PutExtra(Width.idcontenedor, obj.Idcontenedor.ToString());
                    i.PutExtra(Width.contenedor, obj.Contenedor);
                    // startActivity(i);
                    StartActivity(i);

                    //Toast.MakeText(this, "Elimacion cancelada", ToastLength.Short).Show();
                });
                adb.SetNeutralButton("Cancelar", (senderAlert, args) => {

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
                if (num>3)
                {
                    list = ContenedorServ.GetContenedorXnombre(editText1.Text.TrimEnd()).Result;

                    listView1.Adapter = new AutocompleteContenedorAdapter(this, list);
                }
                else
                {
                    list.Clear();// = ContenedorServ.GetContenedorXnombre(editText1.Text.TrimEnd()).Result;

                    listView1.Adapter = new AutocompleteContenedorAdapter(this, list);
                }
              
            }
            catch (System.Exception)
            {

                throw;
            }
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