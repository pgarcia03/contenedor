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
using IngresoSwatch.ModelSqlite;
using Newtonsoft.Json;

namespace IngresoSwatch.Servicios
{
   public class SwatchServ
    {

        /*public static async Task<HttpResponseMessage> Listar( user)
        {
            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "usuario/autenticar");
            //  string url = string.Concat(uri.UrlM, "usuario");

            String js = JsonConvert.SerializeObject(user);

            HttpContent httpContent = new StringContent(js);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync(url, httpContent).ConfigureAwait(false);

            return response;
        }
        */

        public static async Task<HttpResponseMessage> SaveSwatch(SwatchSqlite obj)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "swatch/");

            String js = JsonConvert.SerializeObject(obj);

            HttpContent httpContent = new StringContent(js);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync(url, httpContent).ConfigureAwait(false);

            return response;


        }

        public static async Task<HttpResponseMessage> SaveSwatch(List<SwatchSqlite> obj)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "swatch/");

            String js = JsonConvert.SerializeObject(obj);

            HttpContent httpContent = new StringContent(js);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync(url, httpContent).ConfigureAwait(false);

            return response;


        }
        /*
                public static async Task<List<Usuario>> GetUsuarios()
                {

                    HttpClient httpClient = new HttpClient();
                    var uri = new UrlMain();
                    string url = string.Concat(uri.UrlM, "usuario/");

                    var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var user = UsuarioModel.FromJson(json);

                        var uss = user.Usuarios;

                        return uss;
                    }
                    return null;

                }

                public static async Task<List<Usuario>> GetUsuarios(string parametro, string valor)
                {

                    HttpClient httpClient = new HttpClient();
                    var uri = new UrlMain();
                    string url = string.Concat(uri.UrlM, "usuario/", parametro, '/', valor);

                    var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var user = UsuarioModel.FromJson(json);

                        var uss = user.Usuarios;

                        return uss;
                    }
                    return null;

                }

                public static async Task<string> DeleteUsuarios(string id)
                {

                    HttpClient httpClient = new HttpClient();
                    var uri = new UrlMain();
                    string url = string.Concat(uri.UrlM, "usuario/", id);

                    var response = await httpClient.DeleteAsync(url).ConfigureAwait(false);


                    return response.StatusCode.ToString();

                }*/
    }
}