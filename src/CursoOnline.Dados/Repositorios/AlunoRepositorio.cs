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

		public Aluno ObterPorCpf(string cpf)
		{
			var entidade = Context.Set<Aluno>().Where(c => c.Cpf == cpf);
			if (entidade.Any())
				return entidade.First();
			return null;
		}
	}
}
