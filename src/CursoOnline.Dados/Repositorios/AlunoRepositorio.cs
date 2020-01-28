using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Alunos;
using System.Linq;

namespace CursoOnline.Dados.Repositorios
{
	public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
	{
		public AlunoRepositorio(ApplicationDbContext context) : base(context)
		{
		}

		public Aluno ObeterPeloNome(string nome)
		{
			var entidade = Context.Set<Aluno>().Where(c => c.Nome.Contains(nome));
			if (entidade.Any())
				return entidade.First();
			return null;
		}
	}
}
