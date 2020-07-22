
using SQLite;

namespace IngresoSwatch.ModelSqlite
{
    public class CodigoTelaSqlite
    {
        [PrimaryKey]
        public int idtpc { get; set; }
        public string procod { get; set; }
      //  public int  idcontenedor { get; set; }
    }
}