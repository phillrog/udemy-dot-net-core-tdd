using System.Collections.Generic;
using Xunit;
using ExpectedObjects;

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
				PublicoAlvo = "Estudantes",
				Valor = (double)950,
				CargaHoraria = (double)80
			};

			var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

			cursoEsperado.ToExpectedObject().ShouldMatch(curso);

		}

		private class Curso
		{
			private double valor;
			private double cargaHoraria;
			private string nome;
			private string publicoAlvo;

			public string PublicoAlvo
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


			public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
			{
				this.nome = nome;
				this.cargaHoraria = cargaHoraria;
				this.publicoAlvo = publicoAlvo;
				this.valor = valor;
			}

		}
	}
}
