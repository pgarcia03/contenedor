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
    public class RolloRepositorio: DataBaseContext
    {
        private static object locker = new object();

        public RolloRepositorio()
        {
            CreateDataBase();
        }

        public IEnumerable<RolloSqlite> Usuarios => GetAll();

        public RolloSqlite GetXidRollo(int id)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    return connection.Table<RolloSqlite>().FirstOrDefault(x => x.idrollo.Equals(id));
                }
            }
        }

        public List<RolloSqlite> GetXidcontenedor(int id)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    return connection.Table<RolloSqlite>().Where(x => x.idcontenedor.Equals(id)).ToList();
                }
            }
        }

        public List<RolloSqlite> GetXidcontenedor(int id,int idtela)
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    return connection.Table<RolloSqlite>().Where(x => x.idcontenedor.Equals(id) && x.idtpc==idtela).ToList();
                }
            }
        }
        public void Add(RolloSqlite obj)
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

        public void UpdateStatus(RolloSqlite obj)
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
                    var item = connection.Table<RolloSqlite>().FirstOrDefault(p => p.idrollo == id);
                    if (item != null)
                    {
                        connection.Delete(item);
                    }
                }
            }
        }

        public IEnumerable<RolloSqlite> GetAll()
        {
            lock (locker)
            {
                using (var connection = GetConnection())
                {
                    return connection.Table<RolloSqlite>().ToList();
                }
            }
        }
    }
}