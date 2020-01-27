using Bogus;
using CursoOnline.Dominio._Base;
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
		private Faker _faker;
		private CursoDTO _cursoDTO;
		private Mock<ICursoRepositorio> _cursoRepositorioMock;
		private ArmazenadorDeCurso _armazenadorDeCurso;

		public ArmazenadorDeCursoTest()
		{
			_faker = new Faker();

			_cursoDTO = new CursoDTO
			{
				Nome = _faker.Random.Word(),
				Descricao = _faker.Lorem.Paragraph(),
				CargaHoraria = _faker.Random.Double(50, 1000),
				PublicoAlvo = "Estudante",
				Valor = _faker.Random.Double(1000, 2000)
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

			Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
				.ComMensagem(Resource.NomeCursoJaExiste);
		}

		[Fact]
		public void NaoDeveInformarPublicoAlvoInvalido()
		{
			var publicoAlvoInvalido = "Medico";
			_cursoDTO.PublicoAlvo = publicoAlvoInvalido;

			Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
				.ComMensagem(Resource.PublicoAlvoInvalido);
		}

		[Fact]
		public void DeveAlterarDadosDoCurso()
		{
			_cursoDTO.Id = _faker.Random.Int(1, 999999999);
			var curso = CursoBuilder.Novo().Build();
			_cursoRepositorioMock.Setup(c => c.ObterPorId(_cursoDTO.Id)).Returns(curso);

			_armazenadorDeCurso.Armazenar(_cursoDTO);

			Assert.Equal(_cursoDTO.Nome, curso.Nome);
			Assert.Equal(_cursoDTO.CargaHoraria, curso.CargaHoraria);
			Assert.Equal(_cursoDTO.Valor, curso.Valor);
		}

		[Fact]
		public void NaoDeveAdicionarNoRepositorioQuandoOCursoJaExiste()
		{
			_cursoDTO.Id = _faker.Random.Int(1, 999999999);
			var curso = CursoBuilder.Novo().Build();
			_cursoRepositorioMock.Setup(c => c.ObterPorId(_cursoDTO.Id)).Returns(curso);

			_armazenadorDeCurso.Armazenar(_cursoDTO);

			_cursoRepositorioMock.Verify(c => c.Adicionar(It.IsAny<Curso>()),Times.Never);
		}
	}
}