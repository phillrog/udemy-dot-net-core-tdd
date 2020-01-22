using System;
using System.Collections.Generic;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest: IDisposable
	{
		private readonly ITestOutputHelper _output;
		private readonly string _nome;
		private readonly PublicoAlvo _publicoAlvo;
		private readonly double _valor;
		private readonly double _cargaHoraria;

		public CursoTest(ITestOutputHelper output)
		{
			_output = output;
			_output.WriteLine("COnstrutor sendo inicializado");

			_nome = "Informática básica";
			_publicoAlvo = PublicoAlvo.Estudante;
			_valor = (double)950;
			_cargaHoraria = (double)80;
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
				CargaHoraria = _cargaHoraria
			};

			var curso = new Curso(cursoEsperado.Nome,
						 cursoEsperado.CargaHoraria,
						 cursoEsperado.PublicoAlvo,
						 cursoEsperado.Valor);

			cursoEsperado.ToExpectedObject().ShouldMatch(curso);

		}		

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
		{
			Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido,
						 _cargaHoraria,
						 _publicoAlvo,
						 _valor)).ComMensagem("Nome inválido");

		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida) {


			Assert.Throws<ArgumentException>(() => new Curso(_nome,
						 cargaHorariaInvalida,
						 _publicoAlvo,
						 _valor)).ComMensagem("Carga Horária inválido");
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaValorMenorQue1(double valorInvalido)
		{

			Assert.Throws<ArgumentException>(() => new Curso(_nome,
						 _cargaHoraria,
						 _publicoAlvo,
						 valorInvalido)).ComMensagem("Valor inválido");
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
		private double _cargaHoraria;
		private string _nome;
		private PublicoAlvo _publicoAlvo;

		public PublicoAlvo PublicoAlvo
		{
			get { return _publicoAlvo; }
			set { _publicoAlvo = value; }
		}


		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		public double CargaHoraria
		{
			get { return _cargaHoraria; }
			set { _cargaHoraria = value; }
		}


		public double Valor
		{
			get { return _valor; }
			set { _valor = value; }
		}


		public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
		{
			if (string.IsNullOrEmpty( nome ))
				throw new ArgumentException("Nome inválido");

			if (cargaHoraria < 1)
				throw new ArgumentException("Carga Horária inválido");

			if (valor < 1)
				throw new ArgumentException("Valor inválido");


			this._nome = nome;
			this._cargaHoraria = cargaHoraria;
			this._publicoAlvo = publicoAlvo;
			this._valor = valor;
		}
	}
}
