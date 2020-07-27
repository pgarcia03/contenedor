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
    class CodigosTelaAdapter : BaseAdapter<CodigoTelaModel>
    {

        private readonly Activity _context;
        private readonly List<CodigoTelaModel> _items;

        public CodigosTelaAdapter(Activity context, List<CodigoTelaModel> items) : base()
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

            convertView.FindViewById<TextView>(Resource.Id.txtidcontenedor).Text = item.Idtpc.ToString();
            convertView.FindViewById<TextView>(Resource.Id.txtnombrecontenedor).Text = item.Procod.ToString();

            return convertView;
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count => _items.Count;

        public override CodigoTelaModel this[int position] => _items[position];

        public void RemoveItemAt(int position)
        {
            _items.RemoveAt(position);
        }

    }
}