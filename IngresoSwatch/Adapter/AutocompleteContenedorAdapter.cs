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
using IngresoSwatch.ModelApi;

namespace IngresoSwatch.Adapter
{
    public class AutocompleteContenedorAdapter : BaseAdapter<ContenedorModel>
    {
        private readonly Activity _context;
        private readonly List<ContenedorModel> _items;

        public AutocompleteContenedorAdapter(Activity context, List<ContenedorModel> items) : base()
        {
            _context = context;
            _items = items;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.autocomContenedor, null);

            }

            convertView.FindViewById<TextView>(Resource.Id.txtidcontenedor).Text = item.Idcontenedor;
            convertView.FindViewById<TextView>(Resource.Id.txtnombrecontenedor).Text = item.Contenedor.ToString();
         
            return convertView;
        }

       
        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count => _items.Count;

        public override ContenedorModel this[int position] => _items[position];

        public void RemoveItemAt(int position)
        {
            _items.RemoveAt(position);
        }
    }
}