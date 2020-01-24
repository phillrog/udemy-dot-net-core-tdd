using System;
using CursoOnline.Dominio.Cursos;
using Xunit;
using Moq;
using Bogus;

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
				PublicoAlvo = 1,
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
	}

	public interface ICursoRepositorio
	{
		void Adicionar(Curso curso);
	}

	public class ArmazenadorDeCurso
	{
		private readonly ICursoRepositorio _cursoRepositorio;

		public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
		{
			_cursoRepositorio = cursoRepositorio;
		}

		public void Armazenar(CursoDTO cursoDTO)
		{
			var curso = new Curso(cursoDTO.Nome,
				cursoDTO.CargaHoraria,
				PublicoAlvoEnum.Estudante,
				cursoDTO.Valor,
				cursoDTO.Descricao);

			_cursoRepositorio.Adicionar(curso);
		}
	}

	public class CursoDTO
	{
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public double CargaHoraria { get; set; }
		public int PublicoAlvo { get; set; }
		public double Valor { get; set; }
	}
}
