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
    public class SwatchRepositorio:DataBaseContext
    {
        private static object locker = new object();

        public SwatchRepositorio()
        {
            CreateDataBase();
        }

        public IEnumerable<SwatchSqlite> Usuarios => GetAll();

        public SwatchSqlite GetXidRollo(int id)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    return connection.Table<SwatchSqlite>().FirstOrDefault(x => x.idrollo.Equals(id));
                }
            }
        }

        public SwatchSqlite GetXidswatch(int id)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    return connection.Table<SwatchSqlite>().FirstOrDefault(x => x.idswatches.Equals(id));
                }
            }
        }

        public void Add(SwatchSqlite obj)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    var item = GetXidRollo(obj.idrollo);
                    if (item == null)
                        connection.Insert(obj);
                }
            }
        }

        public void UpdateStatus(SwatchSqlite obj)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    var item = GetXidRollo(obj.idrollo);
                    if (item == null)
                        connection.Insert(obj);
                    else
                    {                      
                        connection.Update(item);
                    }

                }
            }
        }

     
        public void Delete(int id)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    var item = connection.Table<SwatchSqlite>().FirstOrDefault(p => p.idrollo == id);
                    if (item != null)
                    {
                        connection.Delete(item);
                    }
                }
            }
        }

        public IEnumerable<SwatchSqlite> GetAll()
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    return connection.Table<SwatchSqlite>().ToList();
                }
            }
        }
    }
}