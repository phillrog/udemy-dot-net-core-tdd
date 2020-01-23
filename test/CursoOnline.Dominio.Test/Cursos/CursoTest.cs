using System;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;
using Bogus;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest : IDisposable
	{
		private readonly ITestOutputHelper _output;
		private readonly string _nome;
		private readonly PublicoAlvoEnum _publicoAlvo;
		private readonly double _valor;
		private readonly double _cargaHoraria;
		private readonly string _descricao;

		public CursoTest(ITestOutputHelper output)
		{
			_output = output;
			_output.WriteLine("COnstrutor sendo inicializado");

			var faker = new Faker();

			_nome = faker.Random.Word();
			_publicoAlvo = PublicoAlvoEnum.Estudante;
			_valor = faker.Random.Double(50, 1000);
			_cargaHoraria = faker.Random.Double(100, 1000);
			_descricao = faker.Lorem.Paragraph();
		}

		public void Dispose()
		{
			_output.WriteLine("Dispose sendo executado");
		}

		[Fact]
		public void DeveCriarCurso()
		{
			var cursoEsperado = new
			{
				Nome = _nome,
				PublicoAlvo = _publicoAlvo,
				Valor = _valor,
				CargaHoraria = _cargaHoraria,
				Descricao = _descricao
			};

			var curso = new Curso(cursoEsperado.Nome,
						 cursoEsperado.CargaHoraria,
						 cursoEsperado.PublicoAlvo,
						 cursoEsperado.Valor,
						 cursoEsperado.Descricao);

			cursoEsperado.ToExpectedObject().ShouldMatch(curso);

		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
		{
			Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build()).ComMensagem("Nome inválido");
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
		{
			Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
						.ComCargaHoraria(cargaHorariaInvalida).Build()).ComMensagem("Carga Horária inválido");
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaValorMenorQue1(double valorInvalido)
		{

			Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
						.ComValor(valorInvalido).Build()).ComMensagem("Valor inválido");
		}
	}
}
