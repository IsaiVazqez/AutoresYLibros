using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiAutoresyLibros.Controllers;
using WebApiAutoresyLibros.Tests.Mocks;

namespace WebApiAutoresyLibros.Tests.PruebasUnitarias
{
    [TestClass]
    public class RouteControllerTest
    {
        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos4Links()
        {
            //Preparación
            var authorizationSerice = new AuthorizationServiceMock();

            authorizationSerice.Resultado = AuthorizationResult.Success();

            var routeController = new RouteController(authorizationSerice);

            routeController.Url = new URLHelperMock();
            //Ejecución

            var resultado = await routeController.Get();

            //Verificación
            Assert.AreEqual(4, resultado.Value.Count());

        }

        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos2Links()
        {
            //Preparación
            var authorizationSerice = new AuthorizationServiceMock();

            authorizationSerice.Resultado = AuthorizationResult.Failed();

            var routeController = new RouteController(authorizationSerice);

            routeController.Url = new URLHelperMock();
            //Ejecución

            var resultado = await routeController.Get();

            //Verificación
            Assert.AreEqual(2, resultado.Value.Count());

        }
    }
}
