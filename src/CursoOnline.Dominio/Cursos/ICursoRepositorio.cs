using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos
{
	public interface ICursoRepositorio: IRepositorio<Curso>
	{
		Curso ObeterPeloNome(string nome);
	}
}