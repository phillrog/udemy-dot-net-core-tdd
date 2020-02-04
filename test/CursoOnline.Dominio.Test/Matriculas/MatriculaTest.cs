using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicoAlvo;
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
			var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Empreendedor).Build();
			var matriculaEsperada = new
			{
				Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Empreendedor).Build(),
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
			var curso = CursoBuilder.Novo().ComValor(100).ComPublicoAlvo(PublicoAlvoEnum.Empreendedor).Build();
			var valorPagoComDesconto = curso.Valor - 1;

			var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoComDesconto).Build();

			Assert.True(matricula.TemDesconto);
		}

		[Fact]
		public void DevePublicoAlvoDeAlunoECursoSeremIguais()
		{
			var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Estudante).Build();
			var aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Universitario).Build();

			Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Build())
				.ComMensagem(Resource.PublicoAlvoDiferentes);
		}

		[Fact]
		public void DeveInformarANotaDoAlunoParaMatricula()
		{
			const double notDoAlunoEsperada = 9.5;

			var matricula = MatriculaBuilder.Novo().Build();

			matricula.InformarNota(notDoAlunoEsperada);

			Assert.Equal(notDoAlunoEsperada, matricula.NotaDoAluno);
		}

		[Theory]
		[InlineData(11)]
		[InlineData(-1)]
		public void NaoDeveInformarComNotaInvalida(double notaInvalida)
		{
			var matricula = MatriculaBuilder.Novo().Build();

			Assert.Throws<ExcecaoDeDominio>(() => matricula.InformarNota(notaInvalida))
				.ComMensagem(Resource.NotaInvalida);
		}

		[Fact]
		public void DeveIndicarQueCUrsoFoiConcluido()
		{
			const double notDoAlunoEsperada = 9.5;

			var matricula = MatriculaBuilder.Novo().Build();

			matricula.InformarNota(notDoAlunoEsperada);

			Assert.True(matricula.CursoConcluido);
		}

		[Fact]
		public void DeveCancelarMatricula()
		{
			var matricula = MatriculaBuilder.Novo().Build();

			matricula.Cancelar();

			Assert.True(matricula.Cancelada);
		}

		[Fact]
		public void NaoDeveInformarNotaEnquantoMatriculaEstiverCancelada()
		{
			const double notaDoAluno = 3;

			var matricula = MatriculaBuilder.Novo().ComCancelada(true).Build();

			Assert.Throws<ExcecaoDeDominio>(() =>
			matricula.InformarNota(notaDoAluno))
				.ComMensagem(Resource.MatriculaCancelada);
		}

		[Fact]
		public void NaoDeveCancelarQuandoMatriculaEstiverCOncluida()
		{
			const double notaDoAluno = 3;

			var matricula = MatriculaBuilder.Novo().ComConcluida(true).Build();

			Assert.Throws<ExcecaoDeDominio>(() =>
			matricula.Cancelar())
				.ComMensagem(Resource.MatriculaJaConcluida);
		}
	}
}
