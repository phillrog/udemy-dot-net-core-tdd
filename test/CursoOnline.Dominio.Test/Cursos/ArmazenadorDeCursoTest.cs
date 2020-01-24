using System;
using CursoOnline.Dominio.Cursos;
using Xunit;
using Moq;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class ArmazenadorDeCursoTest
	{
		[Fact]
		public void DeveAdicionarOCurso()
		{
			var cursoDTO = new CursoDTO
			{
				Nome = "Curso A",
				Descricao = "Descrição",
				CargaHoraria = 80,
				PublicoAlvo = 1,
				Valor = 850.0
			};

			var cursoRepositorioMock = new Mock<ICursoRepositorio>();

			var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

			armazenadorDeCurso.Armazenar(cursoDTO);

			cursoRepositorioMock.Verify(d => d.Adicionar(It.IsAny<Curso>()));
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
		public int CargaHoraria { get; set; }
		public int PublicoAlvo { get; set; }
		public double Valor { get; set; }
	}
}
