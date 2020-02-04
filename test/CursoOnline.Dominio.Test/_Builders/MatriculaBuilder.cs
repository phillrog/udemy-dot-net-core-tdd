using Bogus;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicoAlvo;

namespace CursoOnline.Dominio.Test._Builders
{
	public class MatriculaBuilder
	{
		private static readonly Faker _faker = new Faker();

		protected Aluno Aluno { get; set; }
		protected Curso Curso { get; set; }
		protected double ValorPago { get; set; }
		protected bool Cancelada { get; set; }
		public bool CursoConcluido { get; private set; }

		public static MatriculaBuilder Novo()
		{
			var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Empreendedor).Build();

			return new MatriculaBuilder()
			{
				Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Empreendedor).Build(),
				Curso = curso,
				ValorPago = curso.Valor
			};
		}

		public MatriculaBuilder ComAluno(Aluno aluno)
		{
			Aluno = aluno;

			return this;
		}

		public MatriculaBuilder ComCurso(Curso curso)
		{
			Curso = curso;

			return this;
		}

		public MatriculaBuilder ComValorPago(double valorPago)
		{
			ValorPago = valorPago;

			return this;
		}
		public MatriculaBuilder ComCancelada(bool cancelada)
		{
			Cancelada = cancelada;

			return this;
		}

		public MatriculaBuilder ComConcluida(bool concluida)
		{
			CursoConcluido = concluida;

			return this;
		}

		public Matricula Build()
		{
			var matricula = new Matricula(Aluno, Curso, ValorPago);

			if(Cancelada)
				matricula.Cancelar();

			if (CursoConcluido)
			{
				const double notaAluno = 7;
				matricula.InformarNota(notaAluno);
			}

			return matricula;
		}

	}
}
