using AutoMapper;
using WebApiAutoresyLibros.Dtoos;
using WebApiAutoresyLibros.Entities;
using WebApiAutoresyLibros.Entitys;

namespace WebApiAutoresyLibros.Utilities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<Autor, AutorDTOConLibros>()
                .ForMember(autorDTO => autorDTO.Libros, opciones => opciones.MapFrom(MapAutorDTOLibros));
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresYLibros, opciones => opciones.MapFrom(MapAutoresYLibros));
            CreateMap<Libro, LibroDTO>();
            CreateMap<Libro, LibroDTOConAutores>()
                .ForMember(libroDTO => libroDTO.Autores, opciones => opciones.MapFrom(MapLibroDTOAutores));
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
        }

        private List<LibroDTO> MapAutorDTOLibros(Autor autor, AutorDTO autorDTO)
        {
            var resultado = new List<LibroDTO>();

            if (autor.autoresYLibros == null) { return resultado; }

            foreach (var autorlibro in autor.autoresYLibros)
            {
                resultado.Add(new LibroDTO()
                {
                    Id = autorlibro.LibroId,
                    Titulo = autorlibro.Libros.Titulo
                });
            }
            return resultado;

        }
        private List<AutorDTO> MapLibroDTOAutores(Libro libros, LibroDTO libroDTO)
        {
            var resultado = new List<AutorDTO>();

            if (libros.AutoresYLibros == null) { return resultado; }

            foreach ( var autorlibro in libros.AutoresYLibros)
            {
                resultado.Add(new AutorDTO()
                {
                    id = autorlibro.AutorId,
                    Name = autorlibro.Autor.Name
                });
            }
            return resultado;
        }

        private List<AutoresYLibros> MapAutoresYLibros(LibroCreacionDTO libroCreacionDTO, Libro libros)
        {
            var resultado = new List<AutoresYLibros>();
            
            if (libroCreacionDTO.AutoresIds == null) { return resultado; }

            foreach (var autorId in libroCreacionDTO.AutoresIds)
                {
                resultado.Add(new AutoresYLibros() { AutorId = autorId });
            }
            return resultado;
        }
    }
}
