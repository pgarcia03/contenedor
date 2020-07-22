
using SQLite;

namespace IngresoSwatch.ModelSqlite
{
    public class SwatchSqlite
    {
        [PrimaryKey][AutoIncrement]
        public int idswatches { get; set; }
        public int idrollo { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double x3 { get; set; }
        public double y1 { get; set; }
        public double y2 { get; set; }
        public double y3 { get; set; }
        public double p1 { get; set; }
        public double p2 { get; set; }
    }
}