using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutoresyLibros.Dtoos;
using WebApiAutoresyLibros.Entitys;

namespace WebApiAutoresyLibros.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutorController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> Get()
        {
            var autores = await context.Autores.ToListAsync();
            
            return mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{id:int}")]//encontrar por id
        public async Task<ActionResult<AutorDTOConLibros>> Get(int id)
        {

            var autor = await context.Autores
                .Include(autorBD => autorBD.autoresYLibros)
                .ThenInclude(autorLibroDB => autorLibroDB.Libros)
                .FirstOrDefaultAsync(autorBD => autorBD.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDTOConLibros>(autor);
        }
        [HttpGet("{nombre}")]//encontrar por nombre
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

        [HttpPost]
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
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
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
