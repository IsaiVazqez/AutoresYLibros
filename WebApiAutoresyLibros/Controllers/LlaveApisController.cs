using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutoresyLibros.Dtoos;
using WebApiAutoresyLibros.Services;

namespace WebApiAutoresyLibros.Controllers
{
    [ApiController]
    [Route("api/llavesapi")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LlaveApisController: CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ServicioLlaves servicioLlaves;

        public LlaveApisController(ApplicationDbContext context, IMapper mapper, ServicioLlaves servicioLlaves)
        {
            this.context = context;
            this.mapper = mapper;
            this.servicioLlaves = servicioLlaves;
        }

        [HttpGet]
        public async Task<List<LlaveDTO>> MisLlaves()
        {
            var usuarioId = ObtenerUsuarioId();

            var llaves = await context.LlavesApi.Where(x => x.UsuarioId == usuarioId).ToListAsync();

            return mapper.Map<List<LlaveDTO>>(llaves);
        }

        [HttpPost]
        public async Task<ActionResult> CrearLlave(CrearLlaveDTO crearLlaveDTO)
        {
            var usuarioId = ObtenerUsuarioId();

            if (crearLlaveDTO.TipoLlave == Entities.TipoLlave.Gratuita)
            {
                var elusuarioYaTieneLlaveGratuita = await context.LlavesApi
                    .AnyAsync(x => x.UsuarioId == usuarioId && x.TipoLlave == Entities.TipoLlave.Gratuita);

                if (elusuarioYaTieneLlaveGratuita)
                {
                    return BadRequest("Ya tienes una llave gratuita");
                }
            }

            await servicioLlaves.CrearLlave(usuarioId, crearLlaveDTO.TipoLlave);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarLlave(ActualizarLlaveDTO actualizarLlaveDTO)
        {
            var usuarioId = ObtenerUsuarioId();

            var llaveDB = await context.LlavesApi.FirstOrDefaultAsync(x => x.Id == actualizarLlaveDTO.LlaveId);

            if (llaveDB == null)
            {
                return NotFound();
            }

            if(usuarioId != llaveDB.UsuarioId)
            {
                return Forbid();
            }

            if(actualizarLlaveDTO.ActualizarLlave)
            {
                llaveDB.Llave = servicioLlaves.GenerarLlave();
            }

            llaveDB.Activa = actualizarLlaveDTO.Activa;

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

