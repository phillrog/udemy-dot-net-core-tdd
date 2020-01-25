using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class ArmazenadorDeCursoTest
	{
		private CursoDTO _cursoDTO;
		private Mock<ICursoRepositorio> _cursoRepositorioMock;
		private ArmazenadorDeCurso _armazenadorDeCurso;

		public ArmazenadorDeCursoTest()
		{
			var fake = new Faker();

			_cursoDTO = new CursoDTO
			{
				Nome = fake.Random.Word(),
				Descricao = fake.Lorem.Paragraph(),
				CargaHoraria = fake.Random.Double(50, 1000),
				PublicoAlvo = "Estudante",
				Valor = fake.Random.Double(1000, 2000)
			};

			_cursoRepositorioMock = new Mock<ICursoRepositorio>();

			_armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
		}

		[Fact]
		public void DeveAdicionarOCurso()
		{
			_armazenadorDeCurso.Armazenar(_cursoDTO);

			_cursoRepositorioMock.Verify(d => d.Adicionar(
				It.Is<Curso>(c =>
					c.Nome.Equals(_cursoDTO.Nome)
				 && c.Descricao.Equals(_cursoDTO.Descricao))));
		}

		[Fact]
		public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
		{
			var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDTO.Nome).Build();

			_cursoRepositorioMock.Setup(c => c.ObeterPeloNome(_cursoDTO.Nome)).Returns(cursoJaSalvo);

			Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
				.ComMensagem("Nome do curso já consta no banco de dados");
		}

		[Fact]
		public void NaoDeveInformarPublicoAlvoInvalido()
		{
			var publicoAlvoInvalido = "Medico";
			_cursoDTO.PublicoAlvo = publicoAlvoInvalido;

			Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
				.ComMensagem("Publico Alvo Inválido");
		}
	}
}