using System;
namespace WebApiAutoresyLibros.Entities
{
    public class Peticion
    {
        public int Id { get; set; }

        public int LlaveId { get; set; }

        public DateTime FechaPeticion { get; set;}

        public LlaveAPI Llave { get; set; }
    }
}

