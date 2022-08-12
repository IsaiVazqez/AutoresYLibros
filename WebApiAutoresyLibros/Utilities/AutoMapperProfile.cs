﻿using AutoMapper;
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
            CreateMap<LibroCreacionDTO, Libros>()
                .ForMember(libro => libro.autoresYLibros, opciones => opciones.MapFrom(MapAutoresYLibros));
            CreateMap<Libros, LibroDTO>();
            CreateMap<Libros, LibroDTOConAutores>()
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
        private List<AutorDTO> MapLibroDTOAutores(Libros libros, LibroDTO libroDTO)
        {
            var resultado = new List<AutorDTO>();

            if (libros.autoresYLibros == null) { return resultado; }

            foreach ( var autorlibro in libros.autoresYLibros)
            {
                resultado.Add(new AutorDTO()
                {
                    id = autorlibro.AutorId,
                    Name = autorlibro.Autor.Name
                });
            }
            return resultado;
        }

        private List<AutoresYLibros> MapAutoresYLibros(LibroCreacionDTO libroCreacionDTO, Libros libros)
        {
            var resultado = new List<AutoresYLibros>();
            
            if (libroCreacionDTO.AutoresIds == null) { return resultado; }

            foreach (var autorID in libroCreacionDTO.AutoresIds)
                {
                resultado.Add(new AutoresYLibros() { AutorId = autorID });
            }
            return resultado;
        }
    }
}
