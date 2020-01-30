using Bogus;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;

namespace CursoOnline.Dominio.Test._Builders
{
	public class MatriculaBuilder
	{
		private Aluno _aluno;
		private Curso _curso;
		private double _valorPago;
		private static readonly Faker _faker = new Faker();

		public Aluno Aluno { get => _aluno; set => _aluno = value; }
		public Curso Curso { get => _curso; set => _curso = value; }
		public double ValorPago { get => _valorPago; set => _valorPago = value; }

		public static MatriculaBuilder Novo()
		{
			var curso = CursoBuilder.Novo().Build();

			return new MatriculaBuilder()
			{
				Aluno = AlunoBuilder.Novo().Build(),
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



		public Matricula Build()
		{
			return new Matricula(Aluno, Curso, ValorPago);
		}

	}
}
