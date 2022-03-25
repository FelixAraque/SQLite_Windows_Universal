using SQLite;

namespace SQLite_Windows_Universal.Model
{
    class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NHoras { get; set; }
        public bool Teletrabajo { get; set; }
        public int Dieta { get; set; }
        public string Notas { get; set; }
    }
}
