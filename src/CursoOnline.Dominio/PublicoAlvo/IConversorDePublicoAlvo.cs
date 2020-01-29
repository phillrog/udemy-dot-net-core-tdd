using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.PublicoAlvo
{
	public interface IConversorDePublicoAlvo
	{
		PublicoAlvoEnum Converter(string publicoAlvo);
	}
}