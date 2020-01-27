using System;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest 
	{
		private readonly ITestOutputHelper _output;
		private readonly Faker _faker;
		private readonly string _nome;
		private readonly PublicoAlvoEnum _publicoAlvo;
		private readonly double _valor;
		private readonly double _cargaHoraria;
		private readonly string _descricao;

		public CursoTest(ITestOutputHelper output)
		{
			_faker = new Faker();

			_nome = _faker.Random.Word();
			_publicoAlvo = PublicoAlvoEnum.Estudante;
			_valor = _faker.Random.Double(50, 1000);
			_cargaHoraria = _faker.Random.Double(100, 1000);
			_descricao = _faker.Lorem.Paragraph();
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
			Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build())
				.ComMensagem(Resource.NomeInvalido);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaCargaHorariaInvalida(double cargaHorariaInvalida)
		{
			Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo()
						.ComCargaHoraria(cargaHorariaInvalida).Build()).ComMensagem(Resource.CargaHorariaInvalida);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaValorInvalido(double valorInvalido)
		{

			Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo()
						.ComValor(valorInvalido).Build()).ComMensagem(Resource.ValorInvalido);
		}

		[Fact]
		public void DeveAlterarNome()
		{
			var nomeEsperado = _faker.Person.FullName;

			var curso = CursoBuilder.Novo().Build();

			curso.AlterarNome(nomeEsperado);

			Assert.Equal(nomeEsperado, curso.Nome);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
		{
			var curso = CursoBuilder.Novo().Build();

			Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarNome(nomeInvalido)).ComMensagem(Resource.NomeInvalido);
		}

		[Fact]
		public void DeveAlterarCargaHoraria()
		{
			var cargaHorariaEsperada = _faker.Random.Double(1, 2000.99);

			var curso = CursoBuilder.Novo().Build();

			curso.AlterarCargaHoraria(cargaHorariaEsperada);

			Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveAlterarCargaHorariaInvalida(double cargaHorariaInvalida)
		{
			var curso = CursoBuilder.Novo().Build();

			Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida))
				.ComMensagem(Resource.CargaHorariaInvalida);
		}

		[Fact]
		public void DeveAlterarValor()
		{
			var valorEsperado = _faker.Random.Double(1, 2000.99);

			var curso = CursoBuilder.Novo().Build();

			curso.AlterarValor(valorEsperado);

			Assert.Equal(valorEsperado, curso.Valor);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveAlterarComValorInvalido(double valorInvalido)
		{
			var curso = CursoBuilder.Novo().Build();

			Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarValor(valorInvalido))
				.ComMensagem(Resource.ValorInvalido);
		}
	}
}
