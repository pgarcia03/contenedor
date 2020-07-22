using System.IO;
using SQLite;

namespace IngresoSwatch.SqliteConexion
{
    public class AndroidSQLite:ISQLite
    {
        private string GetPath()
        {
            var dbName = "teladb.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return path;
        }
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }
        public SQLiteAsyncConnection GetConnectionAsync()
        {
            return new SQLiteAsyncConnection(GetPath());
        }
    }
}