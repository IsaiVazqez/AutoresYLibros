namespace WebApiAutores.Entitys
{
    public class Libro
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AutorId { get; set; }

        public Autor Autor { get; set; }
    }
}
