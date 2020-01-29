using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicoAlvo;
using CursoOnline.Dominio.Test._Util;
using Xunit;

namespace CursoOnline.Dominio.Test.PublicoAlvo
{
	public class ConversorDePublicoAlvoTest
	{
		private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();

		[Theory]
		[InlineData(PublicoAlvoEnum.Empreendedor, "Empreendedor")]
		[InlineData(PublicoAlvoEnum.Estudante, "Estudante")]
		[InlineData(PublicoAlvoEnum.Empregado, "Empregado")]
		[InlineData(PublicoAlvoEnum.Universitario, "Universitario")]
		public void DeveConverterPublicoAlvo(PublicoAlvoEnum publicoAlvoEsperado, string publicoAlvoString)
		{
			var publicoAlvoConvertido = _conversor.Converter(publicoAlvoString);

			Assert.Equal(publicoAlvoConvertido, publicoAlvoEsperado);
		}

		[Fact]
		public void NaoDeveConverterQuandoPublicoAlvoEInvalido()
		{
			const string  publicoAlvoInvalido = "Invalido";

			Assert.Throws<ExcecaoDeDominio>(() => _conversor.Converter(publicoAlvoInvalido))
				.ComMensagem(Resource.PublicoAlvoInvalido);
		}
	}
}
