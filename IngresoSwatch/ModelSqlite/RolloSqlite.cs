using SQLite;

namespace IngresoSwatch.ModelSqlite
{
    public class RolloSqlite
    {
        [PrimaryKey]
        public int idrollo { get; set; }
        public int idtpc { get; set; }
        public int idcontenedor { get; set; }
        public string rolloName { get; set; }
        public int sec { get; set; }
        public int ancho { get; set; }
        public string proceso { get; set; }
        public string estado { get; set; }


    }
}