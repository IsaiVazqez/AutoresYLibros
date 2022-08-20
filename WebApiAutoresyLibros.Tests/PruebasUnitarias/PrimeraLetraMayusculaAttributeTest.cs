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
            //Preparación
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();

            var valor = "felipe";

            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecución
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            //Verificación

            Assert.AreEqual("La primera letra debe ser mayúscula", resultado.ErrorMessage);
        }

        [TestMethod]
        public void valorNulo_NoDevuelveError()
        {
            //Preparación
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();

            string valor = null;

            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecución
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            //Verificación

            Assert.IsNull(resultado);
        }

        [TestMethod]
        public void valorConPirimeraLetraMayuscula_NoDevuelveError()
        {
            //Preparación
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();

            string valor = "Felipe";

            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecución
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            //Verificación

            Assert.IsNull(resultado);
        }
    }
}