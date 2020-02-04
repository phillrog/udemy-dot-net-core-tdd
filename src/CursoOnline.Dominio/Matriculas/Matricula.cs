using System;
using System.Collections.Generic;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
	public class Matricula : Entidade
	{
		private Aluno _aluno;
		private Curso _curso;
		private double _valorPago;
		private double _notaDoAluno;
		private bool _cursoConcluido;
		private bool _matriculaCancelada;
		private bool _matriculaConcluida;

		public Matricula(Aluno aluno, Curso curso, double valorPago)
		{
			ValidadorDeRegra.Novo()
				.Quando(aluno == null, Resource.AlunoInvalido)
				.Quando(curso == null, Resource.CursoInvalido)
				.Quando(valorPago <= 0, Resource.ValorPagoInvalido)
				.Quando(curso != null && valorPago > curso.Valor, Resource.ValorPagoNaoPodeSerMaiorQueValorDoCurso)
				.Quando(aluno != null && curso != null && aluno.PublicoAlvo != curso.PublicoAlvo, Resource.PublicoAlvoDiferentes)
				.DispararExcecaoSeExistir();


			Aluno = aluno;
			Curso = curso;
			ValorPago = valorPago;
			TemDesconto = ValorPago < Curso.Valor;
		}

		public Aluno Aluno { get => _aluno; set => _aluno = value; }
		public Curso Curso { get => _curso; set => _curso = value; }
		public double ValorPago { get => _valorPago; set => _valorPago = value; }
		public bool TemDesconto { get; set; }
		public double NotaDoAluno { get => _notaDoAluno; set => _notaDoAluno = value; }
		public bool CursoConcluido { get => _cursoConcluido; private set => _cursoConcluido = value; }
		public bool Cancelada { get => _matriculaCancelada; private set => _matriculaCancelada = value; }
		public bool Concluida { get => _matriculaConcluida; set => _matriculaConcluida = value; }

		public void InformarNota(double notDoAluno)
		{
			ValidadorDeRegra.Novo()
				.Quando(notDoAluno < 0 || notDoAluno > 10, Resource.NotaInvalida)
				.Quando(_matriculaCancelada, Resource.MatriculaCancelada)
				.DispararExcecaoSeExistir();

			NotaDoAluno = notDoAluno;

			CursoConcluido = true;
		}

		public void Cancelar()
		{
			ValidadorDeRegra.Novo()
				.Quando(CursoConcluido, Resource.MatriculaJaConcluida)
				.DispararExcecaoSeExistir();

			Cancelada = true;
		}
	}
}
