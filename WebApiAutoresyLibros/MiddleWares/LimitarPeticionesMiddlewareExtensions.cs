using System;
using Microsoft.EntityFrameworkCore;
using WebApiAutoresyLibros.Dtoos;
using WebApiAutoresyLibros.Entities;

namespace WebApiAutoresyLibros.MiddleWares
{
    public static class LimitarPeticionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseLimitarPeticiones(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LimitarPeticionesMiddleware>();
        }
    }

    public class LimitarPeticionesMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly IConfiguration configuration;


        public LimitarPeticionesMiddleware(RequestDelegate siguiente, IConfiguration configuration)
        {
            this.siguiente = siguiente;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext, ApplicationDbContext context)
        {
            var limitarPeticionesConfiguracion = new LimitarPeticionesConfiguracion();

            configuration.GetRequiredSection("limitarPeticiones").Bind(limitarPeticionesConfiguracion);

            var ruta = httpContext.Request.Path.ToString();

            var estaLaRutaEnListaBlanca = limitarPeticionesConfiguracion.ListaBlancaRutas.Any(x => ruta.Contains(x));

            if (estaLaRutaEnListaBlanca)
            {
                await siguiente(httpContext);

                return;
            }

            var llaveStringValues = httpContext.Request.Headers["X-Api-Key"];

            if (llaveStringValues.Count == 0)
            {
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync("Debe proveer la llave en la cabecera X-Api-Key");

                return;
            }

            if (llaveStringValues.Count > 1)
            {
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync("Solo una llave debe de estar presente");

                return;
            }

            var llave = llaveStringValues[0];

            //var llaveDB = await context.LlavesApi
            //    .Include(x => x.RestriccionesDominio)
            //    .Include(x => x.RestriccionesIP)
            //    .Include(x => x.Usuario)
            //    .FirstOrDefaultAsync(x => x.Llave == llave);

            //if (llaveDB == null)
            //{
            //    httpContext.Response.StatusCode = 400;

            //    await httpContext.Response.WriteAsync("La llave no existe");

            //    return;
            //}

            //if (!llaveDB.Activa)
            //{
            //    httpContext.Response.StatusCode = 400;
            //    await httpContext.Response.WriteAsync("La llave se encuentra inactiva");
            //    return;
            //}

            //if (llaveDB.TipoLlave == TipoLlave.Gratuita)
            //{
            //    var hoy = DateTime.Today;
            //    var mañana = hoy.AddDays(1);
            //    var cantidadPeticionesRealizadasHoy = await context.Peticiones.CountAsync(x =>
            //    x.LlaveId == llaveDB.Id && x.FechaPeticion >= hoy && x.FechaPeticion < mañana);

            //    if (cantidadPeticionesRealizadasHoy >= limitarPeticionesConfiguracion.PeticionesPorDiaGratuito)
            //    {
            //        httpContext.Response.StatusCode = 429; // Too many requests
            //        await httpContext.Response.WriteAsync("Ha excedido el límite de peticiones por día. Si desea " +
            //            "realizar más peticiones, " +
            //            "actualice su suscripción a una cuenta profesional");
            //        return;
            //    }
            //}
            //else if (llaveDB.Usuario.MalaPaga)
            //{
            //    httpContext.Response.StatusCode = 400;
            //    await httpContext.Response.WriteAsync("El usuario es un mala paga.");
            //    return;
            //}

            //var superaRestricciones = PeticionSuperaAlgunaDeLasRestricciones(llaveDB, httpContext);

            //if (!superaRestricciones)
            //{
            //    httpContext.Response.StatusCode = 403;
            //    return;
            //}

            //var peticion = new Peticion() { LlaveId = llaveDB.Id, FechaPeticion = DateTime.UtcNow };
            //context.Add(peticion);
            //await context.SaveChangesAsync();

            await siguiente(httpContext);
        }
    }

}
