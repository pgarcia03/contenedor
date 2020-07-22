using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using IngresoSwatch.Servicios;
using IngresoSwatch.Adapter;
using System.Collections.Generic;
using IngresoSwatch.ModelApi;

namespace IngresoSwatch
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        AutoCompleteTextView txtsearch;
        List<ContenedorModel> list=new List<ContenedorModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.listaRollos);

            txtsearch = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);

            txtsearch.KeyPress += TextView_KeyPress;
            txtsearch.ItemClick += Txtsearch_ItemClick;
            //var adapter = new ArrayAdapter<String>(this, Resource.Layout.list_item, COUNTRIES);

            //textView.Adapter = adapter;
        }

        private void Txtsearch_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var num = e.Position;

            var id = list[e.Position].Idcontenedor;
        }

        private void TextView_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            var num=txtsearch.Text.Length;

            if (num>3)
            {
                 list = ContenedorServ.GetContenedorXnombre(txtsearch.Text.TrimEnd()).Result;

  //              var adapter = new ArrayAdapter<String>(this, Resource.Layout.autocomContenedor, list);
//                listViewNumeros.Adapter = new ListnumerosAdapter(this, listanumeros);

                txtsearch.Adapter = new AutocompleteContenedorAdapter(this, list);

            }

        }
    }
}