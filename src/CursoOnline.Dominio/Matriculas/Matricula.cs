using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
	public class Matricula
	{
		private Aluno _aluno;
		private Curso _curso;
		private double _valorPago;

		public Matricula(Aluno aluno, Curso curso, double valorPago)
		{
			ValidadorDeRegra.Novo()
				.Quando(aluno == null, Resource.AlunoInvalido)
				.Quando(curso == null, Resource.CursoInvalido)
				.Quando(valorPago <= 0, Resource.ValorPagoInvalido)
				.Quando(curso != null && valorPago > curso.Valor, Resource.ValorPagoNaoPodeSerMaiorQueValorDoCurso)
				.DispararExcecaoSeExistir();


			Aluno = aluno;
			Curso = curso;
			ValorPago = valorPago;
			TemDesconto = ValorPago < Curso.Valor;
		}

		public Aluno Aluno { get => _aluno; set => _aluno = value; }
		public Curso Curso { get => _curso; set => _curso = value; }
		public double ValorPago { get => _valorPago; set => _valorPago = value; }
		public bool TemDesconto { get; internal set; }
	}
}
