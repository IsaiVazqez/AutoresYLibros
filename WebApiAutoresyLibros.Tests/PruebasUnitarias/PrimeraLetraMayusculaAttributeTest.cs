using System.ComponentModel.DataAnnotations;
using WebApiAutoresyLibros.Validaciones;

namespace WebApiAutoresyLibros.Tests.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaAttributeTest
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            //Preparaci�n
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();

            var valor = "felipe";

            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecuci�n
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            //Verificaci�n

            Assert.AreEqual("La primera letra debe ser may�scula", resultado.ErrorMessage);
        }

        [TestMethod]
        public void valorNulo_NoDevuelveError()
        {
            //Preparaci�n
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();

            string valor = null;

            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecuci�n
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            //Verificaci�n

            Assert.IsNull(resultado);
        }

        [TestMethod]
        public void valorConPirimeraLetraMayuscula_NoDevuelveError()
        {
            //Preparaci�n
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();

            string valor = "Felipe";

            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecuci�n
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            //Verificaci�n

            Assert.IsNull(resultado);
        }
    }
}