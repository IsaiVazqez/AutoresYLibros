using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutoresyLibros.Dtoos;
using WebApiAutoresyLibros.Entitys;
using WebApiAutoresyLibros.Utilities;

namespace WebApiAutoresyLibros.Controllers
{
    [ApiController]
    [Route("api/autores")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

    public class AutorController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet(Name = "obtenerAutores")]
        [AllowAnonymous]
        public async Task<ActionResult<List<AutorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Autores.AsQueryable();

            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);

            var autores = await queryable.OrderBy(autor  => autor.Name).Paginar(paginacionDTO).ToListAsync();
            
            return mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{id:int}", Name = "obtenerAutor")]//encontrar por id
        public async Task<ActionResult<AutorDTOConLibros>> Get(int id)
        {

            var autor = await context.Autores
                .Include(autorBD => autorBD.autoresYLibros)
                .ThenInclude(autorLibroDB => autorLibroDB.Libro)
                .FirstOrDefaultAsync(autorBD => autorBD.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDTOConLibros>(autor);
        }
        [HttpGet("{nombre}", Name = "obtenerAutorPorNombre")]//encontrar por nombre
        public async Task<ActionResult<List<AutorDTO>>> Get(string nombre)
        {

            var autores = await context.Autores.Where(autorDB => autorDB.Name.Contains(nombre)).ToListAsync();


            return mapper.Map<List<AutorDTO>>(autores);

        }

        [HttpGet("IdFirst")] //el primero de la lista
        public async Task<ActionResult<Autor>> FirstAuthor()
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpPost(Name = "crearAutor")]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {

            var existeAutorConElMismoNombre = await context.Autores.AnyAsync(x => x.Name == autorCreacionDTO.Name);

            if (existeAutorConElMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre{autorCreacionDTO.Name}");
            }

            var autor = mapper.Map<Autor>(autorCreacionDTO);

            context.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("obtenerAutor", new { id = autor.Id }, autorDTO);
        }

        [HttpPut("{id:int}", Name = "actualizarAutor")]
        public async Task<ActionResult> Put(AutorCreacionDTO autorCreacionDTO, int id)
        {

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var autor = mapper.Map<Autor>(autorCreacionDTO);
            autor.Id = id;

            context.Update(autor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "borrarAutor")]
        public async Task<ActionResult> Delete(int id)

        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            {
                if (!existe)
                {
                    return NotFound();
                }

                context.Remove(new Autor() { Id = id });
                await context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
