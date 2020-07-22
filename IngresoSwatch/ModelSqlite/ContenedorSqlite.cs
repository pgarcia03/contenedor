
using SQLite;

namespace IngresoSwatch.ModelSqlite
{
    public class ContenedorSqlite
    {
        [PrimaryKey]
        public int idcontenedor { get; set; }
        public string contenedor { get; set; }     
        public string estado { get; set; }
      
    }
}