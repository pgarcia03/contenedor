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
using IngresoSwatch.ModelSqlite;

namespace IngresoSwatch.SqliteConexion
{
   public class DataBaseContext:AndroidSQLite
    {
        public bool CreateDataBase()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    //Debes crear aquí cada una de las tablas que necesites
                    connection.CreateTable<CodigoTelaSqlite>();
                    connection.CreateTable<RolloSqlite>();
                    connection.CreateTable<ContenedorSqlite>();
                    connection.CreateTable<SwatchSqlite>();

                }
            }
            catch (Exception e)
            {
                var s = e.Message;
                return false;
            }
            return true;
        }
    }
}