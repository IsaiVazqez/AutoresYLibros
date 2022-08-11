using AutoMapper;
using WebApiAutoresyLibros.Dtoos;
using WebApiAutoresyLibros.Entitys;

namespace WebApiAutoresyLibros.Utilities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<LibroCreacionDTO, Libros>();
            CreateMap<Libros, LibroDTO>();
        }
    }
}
