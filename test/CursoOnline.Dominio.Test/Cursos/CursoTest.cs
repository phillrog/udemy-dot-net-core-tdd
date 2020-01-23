using System;
using System.Collections.Generic;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest : IDisposable
	{
		private readonly ITestOutputHelper _output;
		private readonly string _nome;
		private readonly PublicoAlvo _publicoAlvo;
		private readonly double _valor;
		private readonly double _cargaHoraria;
		private readonly string _descricao;

		public CursoTest(ITestOutputHelper output)
		{
			_output = output;
			_output.WriteLine("COnstrutor sendo inicializado");

			_nome = "Informática básica";
			_publicoAlvo = PublicoAlvo.Estudante;
			_valor = (double)950;
			_cargaHoraria = (double)80;
			_descricao = "Teste";
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

	public enum PublicoAlvo
	{
		Estudante,
		Universitario,
		Epregado,
		Empreendedor
	}

	public class Curso
	{
		private double _valor;
		private string _descricao;
		private double _cargaHoraria;
		private string _nome;
		private PublicoAlvo _publicoAlvo;

		public PublicoAlvo PublicoAlvo { get => _publicoAlvo; set => _publicoAlvo = value; }
		public string Nome { get => _nome; set => _nome = value; }
		public double CargaHoraria { get => _cargaHoraria; set => _cargaHoraria = value; }
		public double Valor { get => _valor; set => _valor = value; }
		public string Descricao { get => _descricao; set => _descricao = value; }

		public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor, string descricao)
		{
			if (string.IsNullOrEmpty(nome))
				throw new ArgumentException("Nome inválido");

			if (cargaHoraria < 1)
				throw new ArgumentException("Carga Horária inválido");

			if (valor < 1)
				throw new ArgumentException("Valor inválido");


			this._nome = nome;
			this._descricao = descricao;
			this._cargaHoraria = cargaHoraria;
			this._publicoAlvo = publicoAlvo;
			this._valor = valor;
		}
	}
}
