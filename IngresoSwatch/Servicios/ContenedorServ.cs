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
    public class ContenedorServ
    {

        public static async Task<List<ContenedorModel>> GetContenedorXnombre(string nombre)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "contenedors/?nombre=" + nombre);

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var contenedor =ContenedorModel.FromJson(json);

                return contenedor;
            }
            return null;

        }

        public static async Task<List<ContenedorModel>> GetContenedor()
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "contenedors/");

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var contenedor = ContenedorModel.FromJson(json);

                return contenedor;
            }
            return null;

        }
    }
}