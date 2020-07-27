using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IngresoSwatch.ModelApi;

namespace IngresoSwatch.Servicios
{
    public class CodigoTelaServ
    {

        public static async Task<List<CodigoTelaModel>> GetCodigosTelaXidContenedor(int idcontenedor)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "TelaCodigos/?", "idcontenedor=", idcontenedor);

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var codigoTelas = CodigoTelaModel.FromJson(json);

                return codigoTelas;
            }
            return null;

        }
    }
}