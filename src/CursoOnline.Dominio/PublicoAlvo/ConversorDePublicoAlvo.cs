using CursoOnline.Dominio._Base;
using System;

namespace CursoOnline.Dominio.PublicoAlvo
{
	public class ConversorDePublicoAlvo: IConversorDePublicoAlvo
	{
		public ConversorDePublicoAlvo()
		{
		}

		public PublicoAlvoEnum Converter(string publicoAlvo)
		{
			ValidadorDeRegra.Novo()
				.Quando(!Enum.TryParse<PublicoAlvoEnum>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
				.DispararExcecaoSeExistir();

			return publicoAlvoConvertido;
		}
	}
}
