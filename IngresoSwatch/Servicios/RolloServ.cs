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
using IngresoSwatch.ModelSqlite;

namespace IngresoSwatch.Servicios
{
    public class RolloServ
    {
        public static async Task<List<RolloSqlite>> GetRollosXidContenedor(string parametro, string valor)
        {

            HttpClient httpClient = new HttpClient();
            var uri = new UrlMain();
            string url = string.Concat(uri.UrlM, "usuario/", parametro, '/', valor);

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                //var user =jsononvert //UsuarioModel.FromJson(json);

                var uss = "";// user.Usuarios;

                return null;// uss;
            }
            return null;

        }
    }
}