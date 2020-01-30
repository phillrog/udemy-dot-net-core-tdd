using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
	public class MatriculaTest
	{
		[Fact]
		public void DeveCriarMatricula()
		{
			var curso = CursoBuilder.Novo().Build();
			var matriculaEsperada = new
			{
				Aluno = AlunoBuilder.Novo().Build(),
				Curso = curso,
				ValorPago = curso.Valor
			};

			var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

			matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
		}

		[Fact]
		public void NaoDeveCriarMatriculaSemAluno()
		{
			Aluno alunoInvalido = null;

			Assert.Throws<ExcecaoDeDominio>(() =>
			MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build())
				.ComMensagem(Resource.AlunoInvalido);
		}

		[Fact]
		public void NaoDeveCriarMatriculaSemCurso()
		{
			Curso cursoInvalido = null;

			Assert.Throws<ExcecaoDeDominio>(() =>
			MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build())
				.ComMensagem(Resource.CursoInvalido);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void NaoDeveCriarMatriculaSemValorPagoInvalido(double valorPagoInvalido)
		{

			Assert.Throws<ExcecaoDeDominio>(() =>
			MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build())
				.ComMensagem(Resource.ValorPagoInvalido);
		}

		[Fact]
		public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorCurso()
		{
			var curso = CursoBuilder.Novo().Build();
			var valorPagoMaiorQueCurso = curso.Valor + 1;

			Assert.Throws<ExcecaoDeDominio>(() =>
			MatriculaBuilder.Novo().ComValorPago(valorPagoMaiorQueCurso).Build())
				.ComMensagem(Resource.ValorPagoNaoPodeSerMaiorQueValorDoCurso);
		}

		[Fact]
		public void DeveIndicarODescontoNaMatricula()
		{
			var curso = CursoBuilder.Novo().ComValor(100).Build();
			var valorPagoComDesconto = curso.Valor - 1;

			var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoComDesconto).Build();

			Assert.True(matricula.TemDesconto);
		}
	}
}
