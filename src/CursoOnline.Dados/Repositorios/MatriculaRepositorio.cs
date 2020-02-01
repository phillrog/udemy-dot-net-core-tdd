using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Matriculas;

namespace CursoOnline.Dados.Repositorios
{
	public class MatriculaRepositorio : RepositorioBase<Matricula>, IMatriculaRepositorio
	{
		public MatriculaRepositorio(ApplicationDbContext context) : base(context)
		{
		}			
	}
}
