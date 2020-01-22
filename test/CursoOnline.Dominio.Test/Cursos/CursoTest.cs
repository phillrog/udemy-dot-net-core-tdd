using System;
using System.Collections.Generic;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest
	{
		[Fact]
		public void DeveCriarCurso()
		{
			var cursoEsperado = new
			{
				Nome = "Informática básica",
				PublicoAlvo = PublicoAlvo.Estudante,
				Valor = (double)950,
				CargaHoraria = (double)80
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
			var cursoEsperado = new
			{
				Nome = nomeInvalido,
				PublicoAlvo = PublicoAlvo.Estudante,
				Valor = (double)950,
				CargaHoraria = (double)80
			};

			var msg = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome,
						 cursoEsperado.CargaHoraria,
						 cursoEsperado.PublicoAlvo,
						 cursoEsperado.Valor)).Message;

			Assert.Equal("Nome inválido", msg);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida) {
			var cursoEsperado = new
			{
				Nome = "Informática Básica",
				PublicoAlvo = PublicoAlvo.Estudante,
				Valor = (double)950,
				CargaHoraria = cargaHorariaInvalida
			};

			var msg = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome,
						 cursoEsperado.CargaHoraria,
						 cursoEsperado.PublicoAlvo,
						 cursoEsperado.Valor)).Message;

			Assert.Equal("Carga Horária inválido", msg);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-100)]
		[InlineData(-2)]
		[InlineData(-1)]
		[InlineData(-1000)]
		public void NaoDeveTerUmaValorMenorQue1(double valorInvalido)
		{
			var cursoEsperado = new
			{
				Nome = "Informática Básica",
				PublicoAlvo = PublicoAlvo.Estudante,
				Valor = valorInvalido,
				CargaHoraria = 100
			};

			var msg = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome,
						 cursoEsperado.CargaHoraria,
						 cursoEsperado.PublicoAlvo,
						 cursoEsperado.Valor)).Message;

			Assert.Equal("Valor inválido", msg);
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
		private double valor;
		private double cargaHoraria;
		private string nome;
		private PublicoAlvo publicoAlvo;

		public PublicoAlvo PublicoAlvo
		{
			get { return publicoAlvo; }
			set { publicoAlvo = value; }
		}


		public string Nome
		{
			get { return nome; }
			set { nome = value; }
		}

		public double CargaHoraria
		{
			get { return cargaHoraria; }
			set { cargaHoraria = value; }
		}


		public double Valor
		{
			get { return valor; }
			set { valor = value; }
		}


		public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
		{
			if (string.IsNullOrEmpty( nome ))
				throw new ArgumentException("Nome inválido");

			if (cargaHoraria < 1)
				throw new ArgumentException("Carga Horária inválido");

			if (valor < 1)
				throw new ArgumentException("Valor inválido");


			this.nome = nome;
			this.cargaHoraria = cargaHoraria;
			this.publicoAlvo = publicoAlvo;
			this.valor = valor;
		}
	}
}
