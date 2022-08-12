using WebApiAutoresyLibros.Entitys;

namespace WebApiAutoresyLibros.Entities
{
    public class AutoresYLibros
    {
        public int LibroId { get; set; }
        public int AutorId { get; set; }
        public int Orden { get; set; }
        public Libros Libros { get; set; }
        public Autor Autor { get; set; }
    }
}
