using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IngresoSwatch.ModelApi;
using Newtonsoft.Json;

namespace IngresoSwatch.Servicios
{
    public class RolloServ
    {
        public static async Task<List<RolloModel>> GetRollosXidContenedor(int idcontenedor, int idtpc)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "rollos/?", "idcontenedor=",idcontenedor.ToString(), "&idtpc=", idtpc.ToString());

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var rollos=RolloModel.FromJson(json);

                
                return rollos;
            }
            return null;

        }

        public static async Task<List<RolloModel>> GetRollosXidContenedor(int idcontenedor)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "rollos/?", "idcontenedor=", idcontenedor.ToString());

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var rollos = RolloModel.FromJson(json);


                return rollos;
            }
            return null;

        }

        public static async Task<List<RolloModel>> GetRollosXidContenedor_ancho(int idcontenedor, int idtpc)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "rollos/ancho/?", "idcontenedor=", idcontenedor.ToString(), "&idtpc=", idtpc.ToString());

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var rollos = RolloModel.FromJson(json);


                return rollos;
            }
            return null;

        }

        public static async Task<List<RolloModel>> GetRollosXidContenedor_ancho(int idcontenedor)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "rollos/ancho/?", "idcontenedor=", idcontenedor.ToString());

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var rollos = RolloModel.FromJson(json);

                return rollos;
            }
            return null;

        }

        public static async Task<HttpResponseMessage> UpdateRollo(RolloModel rollo)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "rollos/?id=",rollo.Idrollo);

            String js = JsonConvert.SerializeObject(rollo);

            HttpContent httpContent = new StringContent(js);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PutAsync(url, httpContent).ConfigureAwait(false);

            return response;


        }
    }
}